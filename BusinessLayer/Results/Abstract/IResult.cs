﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Results.Abstract
{
    public interface IResult
    {
        public string Message { get; }
        public bool Status { get; }
    }
}
