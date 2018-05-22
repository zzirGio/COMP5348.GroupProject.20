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
    public class TransferErrorMessageToTransferErrorItem : IVisitor
    {
        public TransferErrorItem Result { get; set; }
        public void Visit(IVisitable pVisitable)
        {
            if(pVisitable is TransferErrorMessage)
            {
                var message = pVisitable as TransferErrorMessage;
                Result = new TransferErrorItem
                {
                    Error = message.Error,
                    OrderGuid = message.OrderGuid
                };
            }
        }
    }
}
