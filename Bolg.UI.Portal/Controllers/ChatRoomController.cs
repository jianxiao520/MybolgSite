using Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.WebSockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.Http.Results;
using System.Web.WebSockets;

namespace Bolg.UI.Portal.Controllers
{
    public class ChatRoomController : ApiController
    {
        public static Dictionary<string, ChatRoomUserInfo> CONNECT_POOL = new Dictionary<string, ChatRoomUserInfo>();//用户连接池
        public HttpResponseMessage Get(string Method)
        {
            Response<string> Reslut = new Response<string>();
            //接口触发事件
            switch (Method)
            {
                case "Connect":
                    if (HttpContext.Current.IsWebSocketRequest)
                        HttpContext.Current.AcceptWebSocketRequest(ProcessChat);
                    Reslut.State = 1;
                    Reslut.Msg = "连接成功";
                    return new HttpResponseMessage(HttpStatusCode.SwitchingProtocols);
                case "GetUserInfo":
                    Response<Dictionary<string, List<ChatRoomUserInfo>>> ResultData = GetUserInfo(HttpContext.Current.Request.Cookies["ChatRoomToken"] != null ? HttpContext.Current.Request.Cookies["ChatRoomToken"].Value : "");
                    return new HttpResponseMessage()
                    {
                        Content = new StringContent(JsonConvert.SerializeObject(ResultData), Encoding.UTF8, "application/json"),
                    };
                default:
                    Reslut.State = 0;
                    Reslut.Msg = "未知接口";
                    break;
            }
            return new HttpResponseMessage()
            {
                Content = new StringContent(JsonConvert.SerializeObject(Reslut), Encoding.UTF8, "application/json"),
            };
        }
        /// <summary>
        /// 客户端进入处理事件
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        private async Task ProcessChat(AspNetWebSocketContext context)
        {
            ChatRoomUserInfo GetUserData = GenerateUserInfo(
                context.Cookies["UserToken"] != null ? context.Cookies["UserToken"].Value : "",
                context.WebSocket
                );
            try
            {
                #region 新用户进入处理
                //分配用户名给新进用户
                CONNECT_POOL.Add(GetUserData.UserToken, GetUserData);
                //告诉客户端它的UserId
                Send_Msg_Type.Send_LoginSession(GetUserData.UserSocket, GetUserData.UserToken.ToString());
                //判断发送对方是否在线
                foreach (var item in CONNECT_POOL)
                {
                    if (item.Value.UserSocket.State == WebSocketState.Open && item.Value != null && item.Value.UserSocket != GetUserData.UserSocket)
                        Send_Msg_Type.Send_System_Msg(item.Value.UserSocket, "欢迎用户[" + GetUserData.NickName + "]进入本聊天室!!!");
                }
                #endregion
                while (true)
                {
                    if (context.WebSocket.State == WebSocketState.Open)
                    {
                        ArraySegment<byte> buffer = new ArraySegment<byte>(new byte[2048]);
                        WebSocketReceiveResult result = await context.WebSocket.ReceiveAsync(buffer, CancellationToken.None);
                        #region 消息处理（字符截取、消息转发）
                        try
                        {
                            #region 关闭Socket处理，删除连接池
                            //判断是否连接关闭
                            if (context.WebSocket.State != WebSocketState.Open)
                            {
                                if (CONNECT_POOL.ContainsKey(GetUserData.UserToken)) CONNECT_POOL.Remove(GetUserData.UserToken);//删除连接池
                                break;
                            }
                            #endregion
                            string userMsg = Encoding.UTF8.GetString(buffer.Array, 0, result.Count);//发送过来的消息
                            string[] MsgArray = userMsg.Split('|');
                            //判断发送对方是否在线:
                            foreach (var item in CONNECT_POOL)
                            {
                                if (item.Value.UserSocket.State == WebSocketState.Open && item.Value != null && item.Value.UserSocket != GetUserData.UserSocket)
                                    Send_Msg_Type.Send_Other_Msg(item.Value.UserSocket, GetUserData.NickName, MsgArray[1]);
                            }
                        }
                        catch (Exception)
                        {
                            //消息转发异常处理，本次消息忽略 继续监听接下来的消息
                        }
                        #endregion
                    }
                    else
                    {
                        break;
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
        /// <summary>
        /// 创建用户信息
        /// </summary>
        /// <param name="UserToken"></param>
        /// <param name="MySocket"></param>
        /// <returns></returns>
        public ChatRoomUserInfo GenerateUserInfo(string UserToken, WebSocket MySocket)
        {
            try
            {
                //判断是否为游客
                if (UserToken == "") throw new Exception("UserToken空");
                bk_user UserInfo = Common.Cache.CacheHelper.GetCache<bk_user>(UserToken);
                if (UserInfo == null) throw new Exception("UserInfo");
                Random Index = new Random();
                return new ChatRoomUserInfo
                {
                    UserSocket = MySocket,
                    UserToken = UserToken,
                    NickName = UserInfo.Us_NickName,
                    Head_Portrait = UserInfo.Us_ProfilePhoto,
                    Experience = Index.Next(1000, 5000)//经验值随机生成
                };
            }
            catch (Exception)
            {
                return new ChatRoomUserInfo
                {
                    UserToken = Common.MD5Helper.getMD5String(DateTime.Now.ToString()),
                    UserSocket = MySocket,
                    Head_Portrait = "Connection_Fails",
                    NickName = "系统游客"
                };
            }
        }

        /// <summary>
        /// 获取在线用户的数据
        /// </summary>
        /// <param name="UserToken"></param>
        /// <returns></returns>
        public Response<Dictionary<string, List<ChatRoomUserInfo>>> GetUserInfo(string UserToken)
        {
            Dictionary<string, List<ChatRoomUserInfo>> InfoList = new Dictionary<string, List<ChatRoomUserInfo>>();
            Response<Dictionary<string, List<ChatRoomUserInfo>>> ResultData = new Response<Dictionary<string, List<ChatRoomUserInfo>>>();
            try
            {
                //必须要UserToken：必须是通过
                if (CONNECT_POOL.ContainsKey(UserToken))
                {
                    //往返回数据里面塞用户自己信息
                    InfoList.Add("MyInfo", new List<ChatRoomUserInfo> { CONNECT_POOL[UserToken] });
                    List<ChatRoomUserInfo> UsersInfo = new List<ChatRoomUserInfo>();
                    //装载在线用户信息
                    foreach (var item in CONNECT_POOL)
                    {
                        //判断是否为游客
                        if (item.Value.NickName != "系统游客")
                            UsersInfo.Add(item.Value);
                    }

                    //填装到返回数据里面
                    InfoList.Add("UsersInfo", UsersInfo);
                    ResultData.State = 1;
                    ResultData.Msg = InfoList;
                }
                else
                {
                    ResultData.State = 1;
                }
            }
            catch (Exception)
            {
                ResultData.State = 0;
            }
            return ResultData;
        }
    }
    /// <summary>
    /// 发送处理对象
    /// </summary>
    public static class Send_Msg_Type
    {
        /// <summary>
        /// 告诉客户端登录成功并返回UserToken
        /// </summary>
        public static bool Send_LoginSession(WebSocket To, string UserToken)
        {
            try
            {
                Send(To, "0|" + UserToken);
            }
            catch (Exception) { return false; }
            return true;
        }

        /// <summary>
        /// 发送系统消息
        /// </summary>
        /// <param name="To"></param>
        /// <param name="Message"></param>
        /// <returns></returns>
        public static bool Send_System_Msg(WebSocket To, string Message)
        {
            try
            {
                Send(To, "1|" + Message);
            }
            catch (Exception) { return false; }
            return true;
        }

        /// <summary>
        /// 发送其他用户消息
        /// </summary>
        /// <param name="To"></param>
        /// <param name="NickeName"></param>
        /// <param name="Message"></param>
        /// <returns></returns>
        public static bool Send_Other_Msg(WebSocket To, string NickeName, string Message)
        {
            try
            {
                Send(To, "2|" + NickeName + "|" + Message);
            }
            catch (Exception) { return false; }
            return true;
        }

        /// <summary>
        /// 封装发送Socket
        /// </summary>
        /// <param name="To"></param>
        /// <param name="Content"></param>
        /// <returns></returns>
        public static async Task Send(WebSocket To, string Content)
        {
            try
            {
                ArraySegment<byte> buffer = new ArraySegment<byte>(new byte[2048]);
                string userMsg = Content;//发送过来的消息
                buffer = new ArraySegment<byte>(Encoding.UTF8.GetBytes(userMsg));
                await To.SendAsync(buffer, WebSocketMessageType.Text, true, CancellationToken.None);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
