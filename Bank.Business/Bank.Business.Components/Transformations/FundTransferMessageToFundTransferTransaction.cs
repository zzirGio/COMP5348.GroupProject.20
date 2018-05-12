using Bank.Business.Components.Model;
using Common;
using Common.Model;

namespace Bank.Business.Components.Transformations
{
    public class FundTransferMessageToFundTransferTransaction : IVisitor
    {
        public FundTransferTransaction Result { get; set; }

        public void Visit(IVisitable pVisitable)
        {
            if (pVisitable is FundTransferMessage)
            {
                FundTransferMessage lMsg = pVisitable as FundTransferMessage;
                Result = new FundTransferTransaction
                {
                    Amount = lMsg.Amount,
                    FromAcctNumber = lMsg.FromAcctNumber,
                    ToAcctNumber = lMsg.ToAcctNumber
                };
            }
        }
    }
}