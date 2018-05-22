using Common;
using Common.Model;
using DeliveryCo.Services.Transformations;

namespace DeliveryCo.Services
{
    public class SubscriberService : ISubscriberService
    {
        public void PublishToSubscriber(Common.Model.Message pMessage)
        {
            DeliveryService dService = new DeliveryService();
            if (pMessage is DeliveryInfoMessage)
            {
                DeliveryInfoMessage lMessage = pMessage as DeliveryInfoMessage;
                DeliveryInfoMessageToDeliveryInfo lVisitor = new DeliveryInfoMessageToDeliveryInfo();
                lMessage.Accept(lVisitor);
                dService.SubmitDelivery(lVisitor.Result);
            }
        }
    }
}