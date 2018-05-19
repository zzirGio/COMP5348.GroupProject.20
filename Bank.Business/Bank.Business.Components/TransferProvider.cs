﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Bank.Business.Components.Interfaces;
using Bank.Business.Entities;
using System.Transactions;
using Bank.Services.Interfaces;
using Bank.Business.Components.PublisherService;
using Common.Model;

namespace Bank.Business.Components
{
    public class TransferProvider : ITransferProvider
    {


        public void Transfer(double pAmount, int pFromAcctNumber, int pToAcctNumber, Guid pOrderGuid)
        {
            using (TransactionScope lScope = new TransactionScope())
            using (BankEntityModelContainer lContainer = new BankEntityModelContainer())
            {

                try
                {
                    Account lFromAcct = GetAccountFromNumber(pFromAcctNumber);
                    Account lToAcct = GetAccountFromNumber(pToAcctNumber);
                    lFromAcct.Withdraw(pAmount);
                    lToAcct.Deposit(pAmount);
                    lContainer.Attach(lFromAcct);
                    lContainer.Attach(lToAcct);
                    lContainer.ObjectStateManager.ChangeObjectState(lFromAcct, System.Data.EntityState.Modified);
                    lContainer.ObjectStateManager.ChangeObjectState(lToAcct, System.Data.EntityState.Modified);
                    lContainer.SaveChanges();
                    lScope.Complete();

                    TransferCompleteMessage message = new TransferCompleteMessage
                    {
                        OrderGuid = pOrderGuid
                    };
                    message.Topic = "TransferComplete";
                    PublisherServiceClient lClient = new PublisherServiceClient();
                    lClient.Publish(message);
                }
                catch (Exception lException)
                {
                    Console.WriteLine("Error occured while transferring money:  " + lException.Message);

                    TransferErrorMessage message = new TransferErrorMessage
                    {
                        OrderGuid = pOrderGuid
                    };
                    message.Topic = "TransferError";
                    message.Error = lException;
                    PublisherServiceClient lClient = new PublisherServiceClient();
                    lClient.Publish(message);

                    throw;
                }
            }
        }

        private Account GetAccountFromNumber(int pToAcctNumber)
        {
            using (BankEntityModelContainer lContainer = new BankEntityModelContainer())
            {
                return lContainer.Accounts.Where((pAcct) => (pAcct.AccountNumber == pToAcctNumber)).FirstOrDefault();
            }
        }
    }
}
