using Common;
using Common.Model;
using DeliveryCo.MessageTypes;

namespace DeliveryCo.Services.Transformations
{
    public class DeliveryInfoMessageToDeliveryInfo : IVisitor
    {
        public DeliveryInfo Result { get; set; }
        public void Visit(IVisitable pVisitable)
        {
            if (pVisitable is DeliveryInfoMessage)
            {
                DeliveryInfoMessage lMsg = pVisitable as DeliveryInfoMessage;
                Result = new DeliveryInfo()
                {
                    SourceAddress = lMsg.SourceAddress,
                    DestinationAddress = lMsg.DestinationAddress,
                    OrderNumber = lMsg.OrderNumber,
                    DeliveryIdentifier = lMsg.DeliveryIdentifier,
                    DeliveryNotificationAddress = lMsg.DeliveryNotificationAddress,
                    Status = lMsg.Status,
                };
            }
        }
    }
}