using System;
using Common;
using Common.Model;
using DeliveryCo.Business.Components.Model;

namespace DeliveryCo.Business.Components.Transformations
{
    public class DeliverySubmittedInfoToDeliverySubmittedNotification : IVisitor
    {
        private Guid mDeliveryId;

        public DeliverySubmittedNotification Result { get; set; }

        public DeliverySubmittedInfoToDeliverySubmittedNotification(Guid pDeliveryId)
        {
            mDeliveryId = pDeliveryId;
        }

        public void Visit(IVisitable pVisitable)
        {
            if (pVisitable is Model.DeliverySubmittedInfo)
            {
                Model.DeliverySubmittedInfo lMsg = pVisitable as Model.DeliverySubmittedInfo;
                Result = new Common.Model.DeliverySubmittedNotification()
                {
                    Successful = lMsg.Succesful,
                    DeliveryId = mDeliveryId,
                    Topic = lMsg.Topic
                };
            }
        }
    }
}