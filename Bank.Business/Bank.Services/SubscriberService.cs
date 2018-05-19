using Common;
using Common.Model;
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
            TransferService tService = new TransferService();
            tService.Transfer(message.Amount, message.FromAccountNumber, message.ToAccountNumber);
        }
    }
}
