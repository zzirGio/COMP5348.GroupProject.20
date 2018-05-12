using System.Runtime.Serialization;

namespace Common.Model
{
    public class FundTransferMessage : Message
    {
        [DataMember]
        public double Amount { get; set; }

        [DataMember]
        public int FromAcctNumber { get; set; }

        [DataMember]
        public int ToAcctNumber { get; set; }
    }
}