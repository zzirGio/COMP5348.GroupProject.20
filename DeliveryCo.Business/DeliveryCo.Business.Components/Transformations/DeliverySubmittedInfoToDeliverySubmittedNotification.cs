using System;
using Common;
using Common.Model;
using DeliveryCo.Business.Components.Model;

namespace DeliveryCo.Business.Components.Transformations
{
    public class DeliverySubmittedInfoToDeliverySubmittedNotification : IVisitor
    {
        private Guid mDeliveryId;

        public DeliverySubmittedMessage Result { get; set; }

        public DeliverySubmittedInfoToDeliverySubmittedNotification(Guid pDeliveryId)
        {
            mDeliveryId = pDeliveryId;
        }

        public void Visit(IVisitable pVisitable)
        {
            if (pVisitable is Model.DeliverySubmittedInfo)
            {
                Model.DeliverySubmittedInfo lMsg = pVisitable as Model.DeliverySubmittedInfo;
                Result = new Common.Model.DeliverySubmittedMessage()
                {
                    OrderNumber = lMsg.OrderNumber,
                    DeliveryId = mDeliveryId,
                    Topic = lMsg.Topic
                };
            }
        }
    }
}