using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TargetModel
{
    public class HttpResultModel<T> : HttpResultModel where T : class
    {
        public T Data { get; set; }
    }

    public class HttpResultModel
    {
        public int Status { get; set; }

        public string Message { get; set; }
    }
}
