using Common;
using Common.Model;
using VideoStore.Services.MessageTypes.Model;

namespace VideoStore.Services.Transformations
{
    public class DeliverySubmittedMessageToDeliverySubmittedItem : IVisitor
    {
        public DeliverySubmittedItem Result { get; set; }

        public void Visit(IVisitable pVisitable)
        {
            if (pVisitable is DeliverySubmittedMessage)
            {
                DeliverySubmittedMessage lMsg = pVisitable as DeliverySubmittedMessage;
                Result = new DeliverySubmittedItem
                {
                    OrderNumber = lMsg.OrderNumber,
                    DeliveryId = lMsg.DeliveryId
                };
            }
        }
    }
}