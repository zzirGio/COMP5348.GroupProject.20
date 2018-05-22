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
            var oService = new OrderService();
            var dnService = new DeliveryNotificationService();
            if(pMessage.GetType() == typeof(TransferCompleteMessage))
            {
                var lMessage = pMessage as TransferCompleteMessage;
                var lVisitor = new TransferCompleteMessageToTransferCompleteItem();
                lMessage.Accept(lVisitor);
                oService.FundsTransferCompleted(lVisitor.Result);
            }
            else if(pMessage.GetType() == typeof(TransferErrorMessage))
            {
                var lMessage = pMessage as TransferErrorMessage;
                var lVisitor = new TransferErrorMessageToTransferErrorItem();
                lMessage.Accept(lVisitor);
                oService.FundsTransferFailed(lVisitor.Result);
            }
            else if (pMessage.GetType() == typeof(DeliverySubmittedMessage))
            {
                DeliverySubmittedMessage lMessage = pMessage as DeliverySubmittedMessage;
                var lVisitor = new DeliverySubmittedMessageToDeliverySubmittedItem();
                lMessage.Accept(lVisitor);
                oService.DeliverySubmitted(lVisitor.Result);
            }
            else if (pMessage.GetType() == typeof(DeliveryCompletedMessage))
            {
                DeliveryCompletedMessage lMessage = pMessage as DeliveryCompletedMessage;
                var lVisitor = new DeliveryCompletedMessageToDeliveryCompletedItem();
                lMessage.Accept(lVisitor);
                dnService.NotifyDeliveryCompletion(lVisitor.Result.DeliveryIdentifier, (DeliveryInfoStatus)lVisitor.Result.Status);
            }
        }
    }
}
