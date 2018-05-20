using Common;
using Common.Model;
using DeliveryCo.Services.Transformations;

namespace DeliveryCo.Services
{
    public class SubscriberService : ISubscriberService
    {
        public void PublishToSubscriber(Common.Model.Message pMessage)
        {
            if (pMessage is DeliveryInfoMessage)
            {
                DeliveryInfoMessage lMessage = pMessage as DeliveryInfoMessage;
                DeliveryInfoMessageToDeliveryInfo lVisitor = new DeliveryInfoMessageToDeliveryInfo();
                pMessage.Accept(lVisitor);

                DeliveryService deliveryService = new DeliveryService();
                deliveryService.SubmitDelivery(lVisitor.Result);
            }
        }
    }
}