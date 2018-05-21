using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;
using VideoStore.Services.MessageTypes;
using Common.Model;
using VideoStore.Services.MessageTypes.Model;

namespace VideoStore.Services.Interfaces
{
    [ServiceContract]
    public interface IOrderService
    {
        [OperationContract]
        [FaultContract(typeof(InsufficientStockFault))]
        void SubmitOrder(Order pOrder);

        [OperationContract]
        void FundsTransferCompleted(TransferCompleteMessage message);

        [OperationContract]
        void FundsTransferFailed(TransferErrorMessage message);

        [OperationContract]
        void DeliverySubmitted(DeliverySubmittedItem pItem);
    }
}
