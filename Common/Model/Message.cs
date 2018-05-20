using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;


namespace Common.Model
{
    [DataContract]
    [KnownType(typeof(SendEmailMessage))]
    [KnownType(typeof(DeliveryInfoMessage))]
    [KnownType(typeof(DeliverySubmittedNotification))]
    [KnownType(typeof(DeliveryCompletedNotification))]
    [KnownType(typeof(TransferCompleteMessage))]
    [KnownType(typeof(TransferErrorMessage))]
    [KnownType(typeof(TransferRequestMessage))]
    public abstract class Message : IVisitable
    {
        [DataMember]
        public String Topic { get; set; }


        public void Accept(IVisitor pVisitor)
        {
            pVisitor.Visit(this);
        }
    }
}
