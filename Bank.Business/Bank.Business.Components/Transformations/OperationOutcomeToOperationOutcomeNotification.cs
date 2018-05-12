using Bank.Business.Components.Model;
using Bank.Business.Entities;
using Common;
using Common.Model;
using OperationOutcome = Bank.Business.Components.Model.OperationOutcome;

namespace Bank.Business.Components.Transformations
{
    public class OperationOutcomeToOperationOutcomeNotification : IVisitor
    {
        private readonly string mMessage;

        public OperationOutcomeNotification Result { get; set; }

        public OperationOutcomeToOperationOutcomeNotification(string pMessage = null)
        {
            mMessage = pMessage;
        }

        public void Visit(IVisitable pVisitable)
        {
            if (pVisitable is OperationOutcome)
            {
                OperationOutcome lOutcome = pVisitable as OperationOutcome;
                Result = new OperationOutcomeNotification()
                {
                    Topic = lOutcome.Topic,
                    IsSuccessful = lOutcome.IsSuccesful,
                    Message = mMessage
                };
            }
        }
    }
}