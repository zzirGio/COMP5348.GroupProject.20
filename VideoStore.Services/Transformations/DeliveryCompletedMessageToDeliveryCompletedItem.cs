using Common;
using Common.Model;
using VideoStore.Services.MessageTypes.Model;

namespace VideoStore.Services.Transformations
{
    public class DeliveryCompletedMessageToDeliveryCompletedItem : IVisitor
    {
        public DeliveryCompletedItem Result { get; set; }
        public void Visit(IVisitable pVisitable)
        {
            if (pVisitable is DeliveryCompletedMessage)
            {
                DeliveryCompletedMessage lMsg = pVisitable as DeliveryCompletedMessage;
                Result = new DeliveryCompletedItem
                {
                    SourceAddress = lMsg.SourceAddress,
                    DestinationAddress = lMsg.DestinationAddress,
                    OrderNumber = lMsg.OrderNumber,
                    DeliveryIdentifier = lMsg.DeliveryIdentifier,
                    DeliveryNotificationAddress = lMsg.DeliveryNotificationAddress,
                    Status = lMsg.Status
                };
            }
        }
    }
}