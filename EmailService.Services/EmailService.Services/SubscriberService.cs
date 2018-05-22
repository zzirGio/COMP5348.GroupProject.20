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
            EmailService eService = new EmailService();
            if (pMessage is SendEmailMessage)
            {
                SendEmailMessage lMessage = pMessage as SendEmailMessage;
                SendEmailMessageToEmailMessage lVisitor = new SendEmailMessageToEmailMessage();
                lMessage.Accept(lVisitor);

                eService.SendEmail(lVisitor.Result);
            }
        }
    }
}