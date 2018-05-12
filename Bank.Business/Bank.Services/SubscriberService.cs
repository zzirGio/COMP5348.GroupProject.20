using Bank.Business.Components.Transformations;
using Common;
using Common.Model;

namespace Bank.Services
{
    public class SubscriberService : ISubscriberService
    {
        public void PublishToSubscriber(Message pMessage)
        {
            if (pMessage is FundTransferMessage)
            {
                FundTransferMessage lMessage = pMessage as FundTransferMessage;
                FundTransferMessageToFundTransferTransaction lVisitor = new FundTransferMessageToFundTransferTransaction();
                lMessage.Accept(lVisitor);

                var result = lVisitor.Result;
                TransferService lService = new TransferService();
                lService.Transfer(result.Amount, result.FromAcctNumber, result.ToAcctNumber);
            }
        }
    }
}