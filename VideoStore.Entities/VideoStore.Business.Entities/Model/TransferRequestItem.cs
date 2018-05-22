using Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VideoStore.Business.Entities.Model
{
    public class TransferRequestItem : IVisitable
    {
        public String Topic => "TransferRequest";
        public double Amount { get; set; }
        public int FromAccountNumber { get; set; }
        public int ToAccountNumber { get; set; }
        public Guid OrderGuid { get; set; }
        public int CustomerId { get; set; }

        public void Accept(IVisitor pVisitor)
        {
            pVisitor.Visit(this);
        }
    }
}
