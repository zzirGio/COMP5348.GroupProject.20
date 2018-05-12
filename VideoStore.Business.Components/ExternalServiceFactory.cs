﻿using Bank.Services.Interfaces;
using DeliveryCo.Services.Interfaces;
using EmailService.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using VideoStoreMiddleware.Interfaces;

namespace VideoStore.Business.Components
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



        public IEmailService EmailService
        {
            get
            {
                return GetTcpService<IEmailService>("net.tcp://localhost:9040/EmailService");
            }
        }

        public ITransferService TransferService
        {
            get
            {
                return GetTcpService<ITransferService>("net.tcp://localhost:9020/TransferService");
            }
        }

        public IDeliveryService DeliveryService
        {
            get
            {
                return GetTcpService<IDeliveryService>("net.tcp://localhost:9030/DeliveryService");
            }
        }

        public ISubscriptionService SubscriptionService
        {
            get
            {
                return GetTcpService<ISubscriptionService>("net.tcp://localhost:9011/SubscriptionService");

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
            return new ChannelFactory<T>(msmqBinding, address).CreateChannel();
        }
    }
}
