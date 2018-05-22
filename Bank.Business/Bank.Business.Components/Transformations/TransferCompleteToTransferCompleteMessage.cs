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
    public class TransferCompleteToTransferCompleteMessage : IVisitor
    {
        public TransferCompleteMessage Result { get; set; }
        public void Visit(IVisitable pVisitable)
        {
            if(pVisitable is TransferComplete)
            {
                var lItem = pVisitable as TransferComplete;
                Result = new TransferCompleteMessage
                {
                    CustomerId = lItem.CustomerId,
                    OrderGuid = lItem.OrderGuid,
                    Topic = lItem.Topic
                };
            }
        }
    }
}
