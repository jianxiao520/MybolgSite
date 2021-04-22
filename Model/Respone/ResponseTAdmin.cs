using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Respone
{
    public class ResponseTAdmin<T>
    {
        public int code { get; set; }
        public string msg { get; set; }
        public int count { get; set; }
        public T data { get; set; }
    }
}
