using System;
using Common;

namespace VideoStore.Business.Entities.Model
{
    public class EmailMessageItem : IVisitable
    {
        public string Topic => "SendEmail";
        public string ToAddresses { get; set; }
        public string FromAddresses { get; set; }
        public string CCAddresses { get; set; }
        public string BCCAddresses { get; set; }
        public DateTime Date { get; set; }
        public string Message { get; set; }
        public void Accept(IVisitor pVisitor)
        {
            pVisitor.Visit(this);
        }
    }
}