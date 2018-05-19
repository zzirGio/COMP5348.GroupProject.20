using Bank.Services.Interfaces;
using Common;
using Common.Model;
using Microsoft.Practices.ServiceLocation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bank.Services
{
    public class SubscriberService : ISubscriberService
    {
        private ITransferService TransferService
        {
            get { return ServiceLocator.Current.GetInstance<ITransferService>(); }
        }

        public void PublishToSubscriber(Message pMessage)
        {
            TransferRequestMessage message = pMessage as TransferRequestMessage;
            TransferService.Transfer(message.Amount, message.FromAccountNumber, message.ToAccountNumber, message.OrderGuid);
        }
    }
}
