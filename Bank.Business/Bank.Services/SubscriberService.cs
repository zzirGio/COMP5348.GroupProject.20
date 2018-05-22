using Bank.Services.Interfaces;
using Bank.Services.Transformations;
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
            if(pMessage is TransferRequestMessage)
            {
                var message = pMessage as TransferRequestMessage;
                var lVisitor = new TransferRequestMessageToTransferRequest();
                message.Accept(lVisitor);
                var tService = new TransferService();
                tService.Transfer(lVisitor.Result);
            }
        }
    }
}
