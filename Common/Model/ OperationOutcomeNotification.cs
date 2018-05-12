using System;
using System.Runtime.Serialization;

namespace Common.Model
{
    [DataContract]
    public class OperationOutcomeNotification : Message
    {
        [DataMember]
        public bool IsSuccessful { get; set; }

        [DataMember]
        public String Message { get; set; }
    }
}