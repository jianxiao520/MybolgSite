using Bolg.BLL;
using Model;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MyBlog.Controllers
{
    public class IndexController : Controller
    {
        
        // GET: Index
        public ActionResult Index()
        {
            return View();
            //ViewData["ShowUserTrue"] = "";
            //ViewData["ShowUser"]= "<p class=\"iconfont\"></p>登录&注册";
            //bk_user Uid = new bk_user();
            //try
            //{
            //    string LoginToken = Request.Cookies["Token"].V 44         alue;
            //    string LoginUid = Request.Cookies["UsId"].Value;
            //    if (UserBLL.QueryToken(LoginToken) == true)
            //    {
            //        Uid = UserBLL.FindUserData(LoginUid);
            //        //ViewData["ShowUser"] = string.Format("<p style=\"background: url('../UserHeadPortrait/{0}.jpg') no-repeat;background-size: 100% 100%;width: 35px;height: 35px;top: 4px;left: 5px;border-radius: 50%;display: inline-block;margin-right: 10px;position: absolute;\"></p>{1}", Uid.Us_ProfilePhoto, Uid.Us_NickName);
            //       // ViewData["ShowUserTrue"] = "<div class=\"FlyMemu\"><a href=\"MyHome.aspx\">我的信息</a><a href=\"javascript: void(0); \" onclick=\"OutLogin()\">退出登录</a></div>";
            //        ViewBag.ShowUser = string.Format("<p style=\"background: url('../UserHeadPortrait/{0}.jpg') no-repeat;background-size: 100% 100%;width: 35px;height: 35px;top: 4px;left: 5px;border-radius: 50%;display: inline-block;margin-right: 10px;position: absolute;\"></p>{1}", Uid.Us_ProfilePhoto, Uid.Us_NickName);
            //        ViewBag.ShowUserTrue = "<div class=\"FlyMemu\"><a href=\"MyHome.aspx\">我的信息</a><a href=\"javascript: void(0); \" onclick=\"OutLogin()\">退出登录</a></div>";
            //    }
            //}
            //catch
            //{
            //    return View();
            //}

        }
        ///
        public ActionResult Comment()
        {
            return View();
        }
        public ActionResult ChatRoom()
        {


            return View();
        }

        public ActionResult MyWealth()
        {
            return View();
        }
        public ActionResult Register()
        {
            return View();
        }
        public ActionResult Music()
        {
            return View();
        }
        public ActionResult DailyShare()
        {
            return View();
        }
        public ActionResult MyHome()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login()
        {
            HttpRequest Request = System.Web.HttpContext.Current.Request;
            string Type = Request.Form["Type"];
            if (Type == "Login")
            {
                string UserName = Request.Form["U_UserName"];
                string PassWord = Request.Form["U_PassWord"];
                //登录
                var LoginText =new  UserService().Check_Pass(UserName.Trim(), PassWord.Trim());
                JObject Reader = (JObject)JsonConvert.DeserializeObject(LoginText);
                if (Reader["State"].ToString() == "1")
                {
                    //登录成功，帮助客户端设置Cookie
                    HttpCookie cookie = new HttpCookie("Token", Reader["Msg"]["Token"].ToString());
                    cookie.Path = "/";
                    cookie.HttpOnly = false;
                    //添加Cookie过期时间
                    cookie.Expires = DateTime.Now.AddDays(1);
                    Request.Cookies.Add(cookie);

                    HttpCookie cookie1 = new HttpCookie("UsId", Reader["Msg"]["Us_Id"].ToString());
                    cookie1.Path = "/";
                    cookie1.HttpOnly = false;
                    //添加Cookie过期时间
                    cookie1.Expires = DateTime.Now.AddDays(1);
                    Request.Cookies.Add(cookie1);

                    Response.Cookies["UsId"].Value = Reader["Msg"]["Us_Id"].ToString();
                    Response.Cookies["Token"].Value = Reader["Msg"]["Token"].ToString();
                    Response.Cookies["UsId"].Expires = DateTime.Now.AddDays(1);
                    Response.Cookies["username"].Expires = DateTime.Now.AddDays(1);
                }
                //Return_Json = JsonConvert.SerializeObject(Reader);
                // context.Response.Write(Return_Json);
                return Json(LoginText);
            }
            else if (Type == "Register")
            {
                string UserName = Request.Form["U_UserName"];
                string PassWord = Request.Form["U_PassWord"];
                string Phone = Request.Form["U_Phone"];
                string Us_Ip = Request.Form["Us_Ip"];
                //注册
                List<Response<String>> ResponseT = new List<Response<String>>();
                if (new UserService().Register(new bk_user { Us_UserName = UserName, Us_PassWord = PassWord, Us_Phone = Phone, Us_Ip = Us_Ip }) == true)
                { ResponseT.Add(new Response<String> { State = 0, Msg = "注册成功!" }); }
                else { ResponseT.Add(new Response<String> { State = 1, Msg = "用户名已存在，请更换!" }); }
                //  Return_Json = JsonConvert.SerializeObject(ResponseT);
                //context.Response.Write(Return_Json);
                return Json(ResponseT);
            }
            else if (Type == "OutLogin")
            {
                int UsId = Int32.Parse(Request.Form["UsId"]);
                new TokenBLL().DelUserToken(UsId);
                //Request.Write(JsonConvert.SerializeObject(new Response<String> { State = 1, Msg = "删除成功" }));
                return Json(JsonConvert.SerializeObject(new Response<String> { State = 1, Msg = "删除成功" }));
            }
            return Json("123");
        }


    }
}