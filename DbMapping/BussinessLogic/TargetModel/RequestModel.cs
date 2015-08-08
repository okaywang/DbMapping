using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TargetModel
{
    public class RequestModel<T> where T :class
    {
        public TargetTableType TableType { get; set; }

        public T Model { get; set; }
    }
}
