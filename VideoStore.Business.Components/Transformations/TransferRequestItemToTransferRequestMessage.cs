using Common;
using Common.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VideoStore.Business.Entities.Model;

namespace VideoStore.Business.Components.Transformations
{
    public class TransferRequestItemToTransferRequestMessage : IVisitor
    {
        public TransferRequestMessage Result { get; set; }
        public void Visit(IVisitable pVisitable)
        {
            if(pVisitable is TransferRequestItem)
            {
                var item = pVisitable as TransferRequestItem;
                Result = new TransferRequestMessage
                {
                    Amount = item.Amount,
                    CustomerId = item.CustomerId,
                    FromAccountNumber = item.FromAccountNumber,
                    ToAccountNumber = item.ToAccountNumber,
                    OrderGuid = item.OrderGuid,
                    Topic = item.Topic
                };
            }
        }
    }
}
