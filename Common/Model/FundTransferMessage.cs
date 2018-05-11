using System.Runtime.Serialization;

namespace Common.Model
{
    public class FundTransferMessage : Message
    {
        [DataMember]
        public string Item { get; set; }

        [DataMember]
        public double Price { get; set; }

        [DataMember]
        public double Change { get; set; }
    }
}