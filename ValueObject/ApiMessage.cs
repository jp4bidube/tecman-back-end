using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Tecman.ValueObject
{
    public class ApiMessage
    {
        public string Message { get; set; }
        public object Result { get; set; }
        public int ErrorCode { get; set; }
        public bool Success { get; set; }
    }
}
