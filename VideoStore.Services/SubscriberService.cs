using Common;
using Common.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VideoStore.Services.Interfaces;

namespace VideoStore.Services
{
    public class SubscriberService : ISubscriberService
    {
        public void PublishToSubscriber(Message pMessage)
        {
            var oService = new OrderService();
            if(pMessage.GetType() == typeof(TransferCompleteMessage))
            {
                oService.FundsTransferCompleted(pMessage as TransferCompleteMessage);
            }
            else if(pMessage.GetType() == typeof(TransferErrorMessage))
            {
                oService.FundsTransferFailed(pMessage as TransferErrorMessage);
            }
        }
    }
}
