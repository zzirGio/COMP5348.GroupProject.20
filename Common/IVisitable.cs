﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Common
{
    public interface IVisitable
    {
        void Accept(IVisitor pVisitor);
    }
}
