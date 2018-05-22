using Common;
using DeliveryCo.Business.Entities;

namespace DeliveryCo.Business.Components.Model
{
    public class DeliveryCompletedInfo : IVisitable
    {
        public string Topic => "DeliveryCompletedOutcome";
        public DeliveryInfo DeliveryInfo { get; set; }
        public void Accept(IVisitor pVisitor)
        {
            pVisitor.Visit(this);
        }
    }
}