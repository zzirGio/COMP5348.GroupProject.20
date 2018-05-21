using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VideoStore.Business.Components.Interfaces;
using VideoStore.Business.Components.PublisherService;
using VideoStore.Business.Components.Transformations;
using VideoStore.Business.Entities.Model;

namespace VideoStore.Business.Components
{
    public class EmailProvider : IEmailProvider
    {
        public void SendMessage(EmailMessage pMessage)
        {
//            ExternalServiceFactory.Instance.EmailService.SendEmail
//                (
//                    new global::EmailService.MessageTypes.EmailMessage()
//                    {
//                        Message = pMessage.Message,
//                        ToAddresses = pMessage.ToAddress,
//                        Date = DateTime.Now
//                    }
//                );

            PublisherServiceClient lClient = new PublisherServiceClient();
            EmailMessageItem lItem = new EmailMessageItem()
            {
                Date = DateTime.Now,
                Message = pMessage.Message,
                ToAddresses = pMessage.ToAddress
            };
            EmailMessageItemToSendEmailMessage lVisitor = new EmailMessageItemToSendEmailMessage();
            lVisitor.Visit(lItem);
            lClient.Publish(lVisitor.Result);
        }
    }
}
