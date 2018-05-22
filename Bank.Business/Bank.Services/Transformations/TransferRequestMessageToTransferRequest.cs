using Bank.MessageTypes;
using Common;
using Common.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bank.Services.Transformations
{
    public class TransferRequestMessageToTransferRequest : IVisitor
    {
        public TransferRequest Result { get; set; }
        public void Visit(IVisitable pVisitable)
        {
            if(pVisitable is TransferRequestMessage)
            {
                var message = pVisitable as TransferRequestMessage;
                Result = new TransferRequest
                {
                    Amount = message.Amount,
                    CustomerId = message.CustomerId,
                    FromAccountNumber = message.FromAccountNumber,
                    OrderGuid = message.OrderGuid,
                    ToAccountNumber = message.ToAccountNumber
                };
            }
        }
    }
}
