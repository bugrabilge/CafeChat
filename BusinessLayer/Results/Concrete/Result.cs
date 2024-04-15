using BusinessLayer.Results.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Results.Concrete
{
    public class Result : IResult
    {
        public Result(bool status, string message) : this(status)
        {
            Message = message;
        }
        public Result(bool status)
        {
            Status = status;
        }
        public bool Status { get; }
        public string Message { get; }
    }
}
