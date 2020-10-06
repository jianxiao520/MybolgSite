using BLL;
using Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Login
{
    /// <summary>
    /// Login 的摘要说明
    /// </summary>
    public class Login : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {

            string Phone = context.Request.Form["U_Phone"];
            string UserName = context.Request.Form["U_UserName"];
            string PassWord = context.Request.Form["U_PassWord"];
            string Us_Ip = context.Request.Form["Us_Ip"];
            List<Response> ResponseT  = new List<Response>();
            if (Phone != null)
            {
                //注册
                if(UserBLL.Register(new User { Us_UserName = UserName, Us_PassWord = PassWord, Us_Phone = Phone ,Us_Ip = Us_Ip}) == true)
                {
                    ResponseT.Add(new Response { state = 0, msg = "注册成功!" });
                }
                else
                {
                    ResponseT.Add(new Response { state = 1, msg = "用户名已存在，请更换!" });
                }
            }
            else
            {
                //登录
                var LoginText = CheckLogin(UserName, PassWord);
                ResponseT.Add(new Response { state = 1, msg = LoginText });
                if (LoginText == "登录成功")
                {

                }
            }
            context.Response.ContentType = "application/json;charset=utf-8";
            string Return_Json = JsonConvert.SerializeObject(ResponseT);
            context.Response.Write(Return_Json);
        }

        private string CheckLogin(string UserName, string PassWord)
        {
            string Res = UserBLL.Check_Pass(UserName.Trim(), PassWord.Trim());
            if (Res == "0")
            {
                return "欢迎管理员回来!";
            }
            else if (Res != "-1")
            {
                return "登录成功";
            }
            else
            { return "账户密码错误或不存在此用户!"; }
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}