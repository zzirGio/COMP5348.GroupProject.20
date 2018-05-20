﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VideoStore.Business.Components.Interfaces;
using VideoStore.Business.Entities;
using System.Transactions;
using Common.Model;
using Microsoft.Practices.ServiceLocation;
using DeliveryCo.MessageTypes;
using VideoStore.Business.Components.PublisherService;

namespace VideoStore.Business.Components
{
    public class OrderProvider : IOrderProvider
    {
        public IEmailProvider EmailProvider
        {
            get { return ServiceLocator.Current.GetInstance<IEmailProvider>(); }
        }

        public IUserProvider UserProvider
        {
            get { return ServiceLocator.Current.GetInstance<IUserProvider>(); }
        }

        public void SubmitOrder(Order pOrder)
        {
            using (TransactionScope lScope = new TransactionScope())
            {
                LoadMediaStocks(pOrder);
                MarkAppropriateUnchangedAssociations(pOrder);
                using (VideoStoreEntityModelContainer lContainer = new VideoStoreEntityModelContainer())
                {
                    try
                    {
                        Console.WriteLine("Saving temporary");

                        pOrder.OrderNumber = Guid.NewGuid();
                        pOrder.UpdateStockLevels();
                        lContainer.Orders.ApplyChanges(pOrder);

                        TransferFundsFromCustomer(UserProvider.ReadUserById(pOrder.Customer.Id).BankAccountNumber, pOrder.Total ?? 0.0, pOrder.OrderNumber);
                        Console.WriteLine("Funds Transfer Requested");

                        lContainer.SaveChanges();
                        lScope.Complete();

                    }
                    catch (Exception lException)
                    {
                        SendOrderErrorMessage(pOrder, lException);
                        throw;
                    }
                }
            }

            /*TransferFundsFromCustomer(UserProvider.ReadUserById(pOrder.Customer.Id).BankAccountNumber, pOrder.Total ?? 0.0);
            Console.WriteLine("Funds Transfer Requested");
            return;

            using (TransactionScope lScope = new TransactionScope())
            {
                LoadMediaStocks(pOrder);
                MarkAppropriateUnchangedAssociations(pOrder);
                using (VideoStoreEntityModelContainer lContainer = new VideoStoreEntityModelContainer())
                {
                    try
                    {
                        pOrder.OrderNumber = Guid.NewGuid();
                        TransferFundsFromCustomer(UserProvider.ReadUserById(pOrder.Customer.Id).BankAccountNumber, pOrder.Total ?? 0.0);
                        Console.WriteLine("Funds Transfer Requested");

                        // TODO: Refactor this to execute when Bank sends back Successful Notification
                        // pOrder.UpdateStockLevels();
                        // PlaceDeliveryForOrder(pOrder);
                        lContainer.Orders.ApplyChanges(pOrder);
         
                        lContainer.SaveChanges();
                        lScope.Complete();
                        
                    }
                    catch (Exception lException)
                    {
                        SendOrderErrorMessage(pOrder, lException);
                        throw;
                    }
                }
            }
            SendOrderPlacedConfirmation(pOrder);*/
        }

        public void FundsTransferCompleted(TransferCompleteMessage message)
        {
            using (TransactionScope lScope = new TransactionScope())
            {
                using (VideoStoreEntityModelContainer lContainer = new VideoStoreEntityModelContainer())
                {
                    try
                    {
                        Console.WriteLine("Funds Transfer Complete");
                        var pOrder = lContainer.Orders.First(x => x.OrderNumber == message.OrderGuid);

                        PlaceDeliveryForOrder(pOrder);
                        SendOrderPlacedConfirmation(pOrder);

                        lContainer.SaveChanges();
                        lScope.Complete();
                    }
                    catch(Exception lException)
                    {
                        Console.WriteLine("Error in FundsTransferComplete: "+lException.Message);
                        throw;
                    }
                }
            }
            
        }

        public void FundsTransferFailed(TransferErrorMessage message)
        {
            using (TransactionScope lScope = new TransactionScope())
            {
                using (VideoStoreEntityModelContainer lContainer = new VideoStoreEntityModelContainer())
                {
                    try
                    {
                        Console.WriteLine("Funds Transfer Error");
                        var pOrder = lContainer.Orders.First(x => x.OrderNumber == message.OrderGuid);
                        pOrder.MarkAsDeleted();

                        LoadMediaStocks(pOrder);
                        MarkAppropriateUnchangedAssociations(pOrder);

                        lContainer.Orders.ApplyChanges(pOrder);
                        pOrder.UpdateStockLevels();
                        lContainer.SaveChanges();
                        lScope.Complete();
                    }
                    catch(Exception lException)
                    {
                        Console.WriteLine("Error in FundsTransferError: " + lException.Message);
                        throw;
                    }
                }
            }
        }

        private void MarkAppropriateUnchangedAssociations(Order pOrder)
        {
            pOrder.Customer.MarkAsUnchanged();
            pOrder.Customer.LoginCredential.MarkAsUnchanged();
            foreach (OrderItem lOrder in pOrder.OrderItems)
            {
                lOrder.Media.Stocks.MarkAsUnchanged();
                lOrder.Media.MarkAsUnchanged();
            }
        }

        private void LoadMediaStocks(Order pOrder)
        {
            using (VideoStoreEntityModelContainer lContainer = new VideoStoreEntityModelContainer())
            {
                foreach (OrderItem lOrder in pOrder.OrderItems)
                {
                    lOrder.Media.Stocks = lContainer.Stocks.Where((pStock) => pStock.Media.Id == lOrder.Media.Id).FirstOrDefault();    
                }
            }
        }

        

        private void SendOrderErrorMessage(Order pOrder, Exception pException)
        {
            EmailProvider.SendMessage(new EmailMessage()
            {
                ToAddress = pOrder.Customer.Email,
                Message = "There was an error in processsing your order " + pOrder.OrderNumber + ": "+ pException.Message +". Please contact Video Store"
            });
        }

        private void SendOrderPlacedConfirmation(Order pOrder)
        {
            EmailProvider.SendMessage(new EmailMessage()
            {
                ToAddress = pOrder.Customer.Email,
                Message = "Your order " + pOrder.OrderNumber + " has been placed"
            });
        }

        private void PlaceDeliveryForOrder(Order pOrder)
        {
            Delivery lDelivery = new Delivery() { DeliveryStatus = DeliveryStatus.Submitted, SourceAddress = "Video Store Address", DestinationAddress = pOrder.Customer.Address, Order = pOrder };

            Guid lDeliveryIdentifier = ExternalServiceFactory.Instance.DeliveryService.SubmitDelivery(new DeliveryInfo()
            { 
                OrderNumber = lDelivery.Order.OrderNumber.ToString(),  
                SourceAddress = lDelivery.SourceAddress,
                DestinationAddress = lDelivery.DestinationAddress,
                DeliveryNotificationAddress = "net.tcp://localhost:9010/DeliveryNotificationService"
            });

            lDelivery.ExternalDeliveryIdentifier = lDeliveryIdentifier;
            pOrder.Delivery = lDelivery;
            
        }

        private void TransferFundsFromCustomer(int pCustomerAccountNumber, double pTotal, Guid pOrderGuid)
        {
            try
            {
                PublisherServiceClient lClient = new PublisherServiceClient();
                var message = new TransferRequestMessage
                {
                    Topic = "TransferRequest",
                    Amount = pTotal,
                    FromAccountNumber = pCustomerAccountNumber,
                    ToAccountNumber = RetrieveVideoStoreAccountNumber(),
                    OrderGuid = pOrderGuid
                };
                lClient.Publish(message);
            }
            catch(Exception e)
            {
                Console.WriteLine(e);
                throw new Exception("Error Transferring funds for order.");
            }

            /*try
            {
                PublisherServiceClient lClient = new PublisherServiceClient();
                var testMessage = new PriceChangeMessage
                {
                    Change = 0.2,
                    Item = "This is from orderProvider",
                    Price = 0.69,
                    Topic = "TestTopic"
                };
                lClient.Publish(testMessage);
            }
            catch(Exception e)
            {
                Console.WriteLine(e);
                throw new Exception("Error Transferring funds for order.");
            }*/
        }


        private int RetrieveVideoStoreAccountNumber()
        {
            return 123;
        }
    }
}
