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
        private IOrderService OrderService
        {
            get
            {
                return ServiceFactory.GetService<IOrderService>();
            }
        }

        public void PublishToSubscriber(Message pMessage)
        {
            if(pMessage.GetType() == typeof(TransferCompleteMessage))
            {
                OrderService.FundsTransferCompleted(pMessage as TransferCompleteMessage);
            }
            else if(pMessage.GetType() == typeof(TransferErrorMessage))
            {
                OrderService.FundsTransferFailed(pMessage as TransferErrorMessage);
            }
        }
    }
}
