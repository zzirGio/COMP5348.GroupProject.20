using DeliveryCo.Business.Components.Interfaces;

namespace DeliveryCo.Business.Components
{
    public class SubscriberService : ISubscriberService
    {
        public void PublishToSubscriber(Common.Model.Message pMessage)
        {
            if (pMessage is OrderDeliveryMessage)
            {
                OrderDeliveryMessage lMessage = pMessage as OrderDeliveryMessage;
                SendEmailMessageToEmailMessage lVisitor = new SendEmailMessageToEmailMessage();
                pMessage.Accept(lVisitor);

                ServiceLocator.Current.GetInstance<IDeliveryProvider>().SubmitDelivery(
                    MessageTypeConverter.Instance.Convert<
                        global::EmailService.MessageTypes.EmailMessage,
                        global::EmailService.Business.Entities.EmailMessage>(lVisitor.Result));
            }
        }
    }
}