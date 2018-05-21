using System;
using System.Runtime.Serialization;

namespace Common.Model
{
    [DataContract]
    public class DeliverySubmittedMessage : Message
    {
        [DataMember]
        public Guid OrderNumber { get; set; }

        [DataMember]
        public Guid DeliveryId { get; set; }


    }
}