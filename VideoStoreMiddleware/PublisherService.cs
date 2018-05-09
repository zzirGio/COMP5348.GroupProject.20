using System;
using Common;
using Common.Model;
using VideoStoreMiddleware.Interfaces;

namespace VideoStoreMiddleware
{
    public class PublisherService : IPublisherService
    {
        public void Publish(Message pMessage)
        {
            foreach (String lHandlerAddress in SubscriptionRegistry.Instance.GetTopicSubscribers(pMessage.Topic))
            {
                ISubscriberService lSubServ = ServiceFactory.GetService<ISubscriberService>(lHandlerAddress);
                lSubServ.PublishToSubscriber(pMessage);
            }
        }
    }
}