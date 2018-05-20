using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DeliveryCo.Business.Components.Interfaces;
using System.Transactions;
using DeliveryCo.Business.Entities;
using System.Threading;
using DeliveryCo.Business.Components.Model;
using DeliveryCo.Business.Components.PublisherService;
using DeliveryCo.Business.Components.Transformations;
using DeliveryCo.Services.Interfaces;


namespace DeliveryCo.Business.Components
{
    public class DeliveryProvider : IDeliveryProvider
    {
        public void SubmitDelivery(DeliveryCo.Business.Entities.DeliveryInfo pDeliveryInfo)
        {
            using(TransactionScope lScope = new TransactionScope())
            using(DeliveryDataModelContainer lContainer = new DeliveryDataModelContainer())
            {
                pDeliveryInfo.DeliveryIdentifier = Guid.NewGuid();
                pDeliveryInfo.Status = 0;
                lContainer.DeliveryInfoes.AddObject(pDeliveryInfo);
                lContainer.SaveChanges();
                ThreadPool.QueueUserWorkItem(new WaitCallback((pObj) => ScheduleDelivery(pDeliveryInfo)));
                lScope.Complete();
            }
            
            DeliverySubmittedInfo lItem = new DeliverySubmittedInfo {Succesful = true};
            DeliverySubmittedInfoToDeliverySubmittedNotification lVisitor = 
                new DeliverySubmittedInfoToDeliverySubmittedNotification(pDeliveryInfo.DeliveryIdentifier);
            lVisitor.Visit(lItem);
            PublisherServiceClient lClient = new PublisherServiceClient();
            lClient.Publish(lVisitor.Result);
        }

        private void ScheduleDelivery(DeliveryInfo pDeliveryInfo)
        {
            Console.WriteLine("Delivering to" + pDeliveryInfo.DestinationAddress);
            Thread.Sleep(3000);
            //notifying of delivery completion
            using (TransactionScope lScope = new TransactionScope())
            using (DeliveryDataModelContainer lContainer = new DeliveryDataModelContainer())
            {
                pDeliveryInfo.Status = 1;
                DeliveryCompletedInfo lItem = new DeliveryCompletedInfo { DeliveryInfo = pDeliveryInfo };
                DeliveryCompletedInfoToDeliveryCompletedNotification lVisitor = new DeliveryCompletedInfoToDeliveryCompletedNotification();
                lVisitor.Visit(lItem);
                PublisherServiceClient lClient = new PublisherServiceClient();
                lClient.Publish(lVisitor.Result);
            }
        }
    }
}
