using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class UserCookie
    {
        public string Token{ get; set; }
        public int Us_Id { get; set; }
        public DateTime ExpirationDate { get; set; }
    }
}
