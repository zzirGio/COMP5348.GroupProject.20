﻿using Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bank.Business.Components.Model
{
    public class TransferComplete : IVisitable
    {
        public String Topic => "TransferComplete";
        public Guid OrderGuid { get; set; }
        public int CustomerId { get; set; }

        public void Accept(IVisitor pVisitor)
        {
            pVisitor.Visit(this);
        }
    }
}
