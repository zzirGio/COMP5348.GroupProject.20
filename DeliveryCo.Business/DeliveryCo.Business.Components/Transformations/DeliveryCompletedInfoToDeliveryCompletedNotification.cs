using System;
using Common;
using Common.Model;
using DeliveryCo.Business.Components.Model;

namespace DeliveryCo.Business.Components.Transformations
{
    public class DeliveryCompletedInfoToDeliveryCompletedNotification : IVisitor
    {
        public DeliveryCompletedNotification Result { get; set; }

        public void Visit(IVisitable pVisitable)
        {
            if (pVisitable is DeliveryCompletedInfo)
            {
                DeliveryCompletedInfo lMsg = pVisitable as DeliveryCompletedInfo;
                Result = new DeliveryCompletedNotification()
                {
                    SourceAddress = lMsg.DeliveryInfo.SourceAddress,
                    DestinationAddress = lMsg.DeliveryInfo.DestinationAddress,
                    OrderNumber = lMsg.DeliveryInfo.OrderNumber,
                    DeliveryIdentifier = lMsg.DeliveryInfo.DeliveryIdentifier,
                    DeliveryNotificationAddress = lMsg.DeliveryInfo.DeliveryNotificationAddress,
                    Status = lMsg.DeliveryInfo.Status,
                    Topic = lMsg.Topic
                };
            }
        }
    }
}