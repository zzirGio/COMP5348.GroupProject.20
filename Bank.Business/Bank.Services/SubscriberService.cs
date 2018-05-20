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
        public void PublishToSubscriber(Message pMessage)
        {
            TransferRequestMessage message = pMessage as TransferRequestMessage;
            var tService = new TransferService();
            tService.Transfer(message.Amount, message.FromAccountNumber, message.ToAccountNumber, message.OrderGuid, message.CustomerId);
        }
    }
}
