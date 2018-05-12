using Common;
using Common.Model;
using VideoStore.Business.Components.Model;

namespace VideoStore.Business.Components.Transformation
{
    public class FundTransferRequestToFundTransferMessage : IVisitor
    {
        public FundTransferMessage Result { get; set; }

        public void Visit(IVisitable pVisitable)
        {
            if (pVisitable is FundTransferRequest)
            {
                FundTransferRequest lRequest = pVisitable as FundTransferRequest;
                Result = new FundTransferMessage
                {
                    Topic = lRequest.Topic,
                    Amount = lRequest.Amount,
                    FromAcctNumber = lRequest.FromAcctNumber,
                    ToAcctNumber = lRequest.ToAcctNumber
                };
            }
        }
    }
}