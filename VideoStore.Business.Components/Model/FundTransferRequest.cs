using Common;

namespace VideoStore.Business.Components.Model
{
    public class FundTransferRequest : IVisitable
    {
        public string Topic => "FundTransfer";
        public double Amount { get; set; }
        public int FromAcctNumber { get; set; }
        public int ToAcctNumber { get; set; }

        public void Accept(IVisitor pVisitor)
        {
            pVisitor.Visit(this);
        }
    }
}