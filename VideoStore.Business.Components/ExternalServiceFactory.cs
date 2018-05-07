﻿using Bank.Services.Interfaces;
using DeliveryCo.Services.Interfaces;
using EmailService.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;

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
        // TODO: change to MSMSQ Service
        public ITransferService TransferService
        {
            get
            {
                return GetMsmqService<ITransferService>("net.msmq://localhost/private/TransferService");
            }
        }

        public IDeliveryService DeliveryService
        {
            get
            {
                return GetTcpService<IDeliveryService>("net.tcp://localhost:9030/DeliveryService");
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
