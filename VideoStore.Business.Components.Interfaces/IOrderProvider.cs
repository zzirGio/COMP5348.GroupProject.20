using Common.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VideoStore.Business.Entities;

namespace VideoStore.Business.Components.Interfaces
{
    public interface IOrderProvider
    {
        void SubmitOrder(Order pOrder);
        void FundsTransferCompleted(TransferCompleteMessage message);
        void FundsTransferFailed(TransferErrorMessage message);
    }
}
