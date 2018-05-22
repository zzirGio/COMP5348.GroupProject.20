using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Bank.Business.Components.Interfaces;
using Bank.Business.Entities;
using System.Transactions;
using Bank.Services.Interfaces;
using Bank.Business.Components.PublisherService;
using Common.Model;
using Bank.Business.Components.Model;
using Bank.Business.Components.Transformations;

namespace Bank.Business.Components
{
    public class TransferProvider : ITransferProvider
    {


        public void Transfer(double pAmount, int pFromAcctNumber, int pToAcctNumber, Guid pOrderGuid, int pCustomerId)
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

                    var lItem = new TransferComplete
                    {
                        OrderGuid = pOrderGuid,
                        CustomerId = pCustomerId
                    };
                    var lVisitor = new TransferCompleteToTransferCompleteMessage();
                    lItem.Accept(lVisitor);
                    PublisherServiceClient lClient = new PublisherServiceClient();
                    lClient.Publish(lVisitor.Result);
                    
                }
                catch (Exception lException)
                {
                    Console.WriteLine("Error occured while transferring money:  " + lException.Message);

                    var lItem = new TransferError
                    {
                        OrderGuid = pOrderGuid
                    };
                    var lVisitor = new TransferErrorToTransferErrorMessage();
                    lItem.Accept(lVisitor);
                    PublisherServiceClient lClient = new PublisherServiceClient();
                    lClient.Publish(lVisitor.Result);

                }

                lContainer.SaveChanges();
                lScope.Complete();
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
