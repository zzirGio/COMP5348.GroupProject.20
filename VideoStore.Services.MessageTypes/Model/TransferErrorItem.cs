using Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VideoStore.Services.MessageTypes.Model
{
    public class TransferErrorItem : IVisitable
    {
        public Exception Error { get; set; }
        public Guid OrderGuid { get; set; }

        public void Accept(IVisitor pVisitor)
        {
            pVisitor.Visit(this);
        }
    }
}
