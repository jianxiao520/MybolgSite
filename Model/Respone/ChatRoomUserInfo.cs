using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.WebSockets;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class ChatRoomUserInfo
    {
        public string UserToken { get; set; }
        //用户连接
        public WebSocket UserSocket { get; set; }
        //用户昵称
        public string NickName { get; set; }
        //用户头像
        public string Head_Portrait { get; set; }
        //用户经验值
        public int Experience { get; set; }
    }
}
