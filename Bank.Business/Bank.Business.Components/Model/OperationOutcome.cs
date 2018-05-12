using Common;

namespace Bank.Business.Components.Model
{
    public class OperationOutcome : IVisitable
    {
        public string Topic => "OutcomeNotification";
        public bool IsSuccesful { get; set; }
        public string Message { get; set; }

        public void Accept(IVisitor pVisitor)
        {
            pVisitor.Visit(this);
        }
    }
}