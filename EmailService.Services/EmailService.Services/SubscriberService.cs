using System;
using Common;
using Common.Model;
using EmailService.Business.Components.Interfaces;
using EmailService.Services.Transformations;

namespace EmailService.Services
{
    public class SubscriberService : ISubscriberService
    {
        public void PublishToSubscriber(Common.Model.Message pMessage)
        {
            if (pMessage is SendEmailMessage)
            {
                SendEmailMessage lMessage = pMessage as SendEmailMessage;
                SendEmailMessageToEmailMessage lVisitor = new SendEmailMessageToEmailMessage();
                pMessage.Accept(lVisitor);

                ServiceFactory.GetService<IEmailProvider>().SendEmail(
                    MessageTypeConverter.Instance.Convert<
                        global::EmailService.MessageTypes.EmailMessage,
                        global::EmailService.Business.Entities.EmailMessage>(lVisitor.Result));
            }
        }
    }
}