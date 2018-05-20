using Common;
using Common.Model;
using VideoStore.Business.Components.Model;

namespace VideoStore.Business.Components.Transformations
{
    public class DeliveryInfoItemToDeliveryInfoMessage : IVisitor
    {
        public DeliveryInfoMessage Result { get; set; }
        public void Visit(IVisitable pVisitable)
        {
            if (pVisitable is DeliveryInfoItem)
            {
                DeliveryInfoItem lItem = pVisitable as DeliveryInfoItem;
                DeliveryInfoMessage Result = new DeliveryInfoMessage
                {
                    SourceAddress = lItem.SourceAddress,
                    DestinationAddress = lItem.DestinationAddress,
                    OrderNumber = lItem.OrderNumber,
                    DeliveryIdentifier = lItem.DeliveryIdentifier,
                    DeliveryNotificationAddress = lItem.DeliveryNotificationAddress,
                    Status = lItem.Status,
                    Topic = lItem.Topic
                };
            }
        }
    }
}