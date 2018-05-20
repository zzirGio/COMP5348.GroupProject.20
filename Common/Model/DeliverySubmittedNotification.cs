using System;
using System.Runtime.Serialization;

namespace Common.Model
{
    [DataContract]
    public class DeliverySubmittedNotification : Message
    {
        [DataMember]
        public bool Successful { get; set; }

        [DataMember]
        public Guid DeliveryId { get; set; }
    }
}