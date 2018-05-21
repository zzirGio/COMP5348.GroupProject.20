using Common;
using Common.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DeliveryCo.Services.Interfaces;
using VideoStore.Services.Interfaces;
using VideoStore.Services.Transformations;

namespace VideoStore.Services
{
    public class SubscriberService : ISubscriberService
    {
        public void PublishToSubscriber(Message pMessage)
        {
            // TODO: apply Transformations from Common::Message models to MessageTypes.Model models
            var oService = new OrderService();
            var dnService = new DeliveryNotificationService();
            if(pMessage.GetType() == typeof(TransferCompleteMessage))
            {
                oService.FundsTransferCompleted(pMessage as TransferCompleteMessage);
            }
            else if(pMessage.GetType() == typeof(TransferErrorMessage))
            {
                oService.FundsTransferFailed(pMessage as TransferErrorMessage);
            } else if (pMessage.GetType() == typeof(DeliverySubmittedMessage))
            {
                DeliverySubmittedMessage lMessage = pMessage as DeliverySubmittedMessage;
                var lVisitor = new DeliverySubmittedMessageToDeliverySubmittedItem();
                pMessage.Accept(lVisitor);
                oService.DeliverySubmitted(lVisitor.Result);
            }
            else if (pMessage.GetType() == typeof(DeliveryCompletedMessage))
            {
                DeliveryCompletedMessage lMessage = pMessage as DeliveryCompletedMessage;
                var lVisitor = new DeliveryCompletedMessageToDeliveryCompletedItem();
                pMessage.Accept(lVisitor);
                dnService.NotifyDeliveryCompletion(lVisitor.Result.DeliveryIdentifier, (DeliveryInfoStatus)lVisitor.Result.Status);
            }
        }
    }
}
