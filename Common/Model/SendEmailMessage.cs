using System;
using System.Runtime.Serialization;

namespace Common.Model
{
    [DataContract]
    public class SendEmailMessage : Message
    {
        [DataMember]
        public String ToAddresses { get; set; }
        [DataMember]
        public String FromAddresses { get; set; }
        [DataMember]
        public String CCAddresses { get; set; }
        [DataMember]
        public String BCCAddresses { get; set; }
        [DataMember]
        public DateTime Date { get; set; }
        [DataMember]
        public String Message { get; set; }
    }
}