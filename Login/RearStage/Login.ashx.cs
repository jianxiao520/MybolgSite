using Bolg.BLL;
using Model;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
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
            context.Response.ContentType = "application/json;charset=utf-8";

            string Type = context.Request.Form["Type"];
            string Return_Json = "";
            if (Type == "Login")
            {
                string UserName = context.Request.Form["U_UserName"];
                string PassWord = context.Request.Form["U_PassWord"];
                //登录
                var LoginText = new UserService().Check_Pass(UserName.Trim(), PassWord.Trim());
                JObject Reader = (JObject)JsonConvert.DeserializeObject(LoginText);
                if (Reader["State"].ToString() == "1")
                {
                    //登录成功，帮助客户端设置Cookie
                    HttpCookie cookie = new HttpCookie("Token",Reader["Msg"]["Token"].ToString());
                    cookie.Path = "/";
                    cookie.HttpOnly = false;
                    //添加Cookie过期时间
                    cookie.Expires = DateTime.Now.AddDays(1);
                    HttpContext.Current.Response.Cookies.Add(cookie);

                    HttpCookie cookie1 = new HttpCookie("UsId", Reader["Msg"]["Us_Id"].ToString());
                    cookie1.Path = "/";
                    cookie1.HttpOnly = false;
                    //添加Cookie过期时间
                    cookie1.Expires = DateTime.Now.AddDays(1);
                    HttpContext.Current.Response.Cookies.Add(cookie1);
                }
                Return_Json = JsonConvert.SerializeObject(Reader);
                context.Response.Write(Return_Json);
            }
            else if(Type == "Register")
            {
                string UserName = context.Request.Form["U_UserName"];
                string PassWord = context.Request.Form["U_PassWord"];
                string Phone = context.Request.Form["U_Phone"];
                string Us_Ip = context.Request.Form["Us_Ip"];
                //注册
                List<Response<String>> ResponseT = new List<Response<String>>();
                if (new UserService().Register(new bk_user { Us_UserName = UserName, Us_PassWord = PassWord, Us_Phone = Phone, Us_Ip = Us_Ip }) == true)
                { ResponseT.Add(new Response<String> { State = 0, Msg = "注册成功!" }); }
                else { ResponseT.Add(new Response<String> { State = 1, Msg = "用户名已存在，请更换!" }); }
                Return_Json = JsonConvert.SerializeObject(ResponseT);
                context.Response.Write(Return_Json);
            }else if (Type == "OutLogin")
            {
                int UsId =Int32.Parse( context.Request.Form["UsId"]);
               new  TokenBLL().DelUserToken(UsId);
                context.Response.Write(JsonConvert.SerializeObject(new Response<String> { State = 1, Msg = "删除成功" }));
            }
            
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