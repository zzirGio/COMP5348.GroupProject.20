using Bank.Business.Components.Model;
using Common;
using Common.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bank.Business.Components.Transformations
{
    public class TransferErrorToTransferErrorMessage : IVisitor
    {
        public TransferErrorMessage Result { get; set; }
        public void Visit(IVisitable pVisitable)
        {
            if(pVisitable is TransferError)
            {
                var lItem = pVisitable as TransferError;
                Result = new TransferErrorMessage
                {
                    Error = lItem.Error,
                    OrderGuid = lItem.OrderGuid,
                    Topic = lItem.Topic
                };
            }
        }
    }
}
