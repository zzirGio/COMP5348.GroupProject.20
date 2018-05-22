using Common;
using Common.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VideoStore.Services.MessageTypes.Model;

namespace VideoStore.Services.Transformations
{
    public class TransferCompleteMessageToTransferCompleteItem : IVisitor
    {
        public TransferCompleteItem Result { get; set; }
        public void Visit(IVisitable pVisitable)
        {
            if(pVisitable is TransferCompleteMessage)
            {
                var message = pVisitable as TransferCompleteMessage;
                Result = new TransferCompleteItem
                {
                    OrderGuid = message.OrderGuid
                };
            }
        }
    }
}
