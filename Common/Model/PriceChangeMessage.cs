using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace Common.Model
{
    [DataContract]
    public class PriceChangeMessage : Message
    {

        [DataMember]
        public string Item { get; set; }

        [DataMember]
        public double Price { get; set; }

        [DataMember]
        public double Change { get; set; }
    }
}
