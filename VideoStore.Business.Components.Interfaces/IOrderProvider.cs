using Common.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VideoStore.Business.Entities;
using VideoStore.Business.Entities.Model;

namespace VideoStore.Business.Components.Interfaces
{
    public interface IOrderProvider
    {
        void SubmitOrder(Order pOrder);
        void FundsTransferCompleted(Guid pOrderGuid);
        void FundsTransferFailed(Guid pOrderGuid);

        void DeliverySubmitted(Guid pOrderNumber, Guid pDeliveryIdentifier);
    }
}
