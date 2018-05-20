using System;
using Common;

namespace DeliveryCo.Business.Components.Model
{
    public class DeliverySubmittedInfo : IVisitable
    {
        public string Topic => "DeliverySubmittedOutcome";
        public bool Succesful { get; set; }
        public Guid DeliveryId { get; set; }

        public void Accept(IVisitor pVisitor)
        {
            pVisitor.Visit(this);
        }
    }
}