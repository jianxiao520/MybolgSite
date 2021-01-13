using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class Response<T>
    {

        public int State { get; set; }
        public T Msg { get; set; }
    }
}
