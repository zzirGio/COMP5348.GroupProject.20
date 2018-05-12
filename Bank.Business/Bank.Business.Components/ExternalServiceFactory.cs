using System;
using System.ServiceModel;
using Bank.Services.Interfaces;
using VideoStoreMiddleware.Interfaces;

namespace Bank.Business.Components
{
    public class ExternalServiceFactory
    {
        private static ExternalServiceFactory sFactory = new ExternalServiceFactory();

        public static ExternalServiceFactory Instance
        {
            get
            {
                return sFactory;
            }
        }

        public IPublisherService PublisherService
        {
            get
            {
                return GetMsmqService<IPublisherService>(
                    "net.msmq://localhost/private/PublisherMessageQueueTransacted");
            }
        }

        public ISubscriptionService SubscriptionService
        {
            get
            {
                return GetTcpService<ISubscriptionService>("net.tcp://localhost:9011/SubscriptionService");

            }
        }

        private T GetTcpService<T>(String pAddress)
        {
            NetTcpBinding tcpBinding = new NetTcpBinding() { TransactionFlow = true };
            EndpointAddress address = new EndpointAddress(pAddress);
            return new ChannelFactory<T>(tcpBinding, pAddress).CreateChannel();
        }

        private T GetMsmqService<T>(String pAddress)
        {
            NetMsmqBinding msmqBinding = new NetMsmqBinding(NetMsmqSecurityMode.None) { Durable = true };
            EndpointAddress address = new EndpointAddress(pAddress);
            return new ChannelFactory<T>(msmqBinding, pAddress).CreateChannel();
        }
    }
}