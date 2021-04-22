using Bolg.BLL;
using Bolg.DAL;
using Bolg.UI.Portal.Controllers;
using IBLL;
using IDAL;
using Model;
using Model.Respone;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Bolg.UI.Portal
{
    public class BackEndController : BaseController
    {
        public IUserBLL UserService_ { get; set; }
        public ActionResult Index()
        {
            if (UserIsLogin == true && Sso_UserInfo.Us_UserName == "admin")
            { return View(); }
            else
            { return Content("<a style=' font-size: 78px;text-align: center;margin-top: 100px;display: list-item;' href=../Index>您不是管理员，无权打开本页面！<a>", "text/html"); }
        }

        public ActionResult workplace()
        {

            return View();
        }
        public ActionResult UserList()
        {
            if (UserIsLogin == true && Sso_UserInfo.Us_UserName == "admin")
            { return View(); }
            else
            { return Content("<h1>您不是管理员，无权打开本页面！<h1>", "text/html"); }
        }
        public ActionResult member_list1()
        {
            return View();
        }
        public ActionResult UserInfo(string Method, string page, string limit, string Us_Id, string key, string value)
        {
            int Count = 0;
            switch (Method)
            {
                case "GetUserInfo":
                    List<bk_user> UserInfo = new List<bk_user>();
                    UserInfo = UserService_.QueryAllData(Int32.Parse(page), Int32.Parse(limit), out Count).ToList();
                    ResponseTAdmin<List<bk_user>> ResponseAllUserInfo = new ResponseTAdmin<List<bk_user>>
                    {
                        code = 0,
                        count = Count,
                        msg = "",
                        data = UserInfo
                    };
                    return Json(ResponseAllUserInfo, JsonRequestBehavior.AllowGet);
                case "AlterInfo":
                    Response<String> ResponseContent = new Response<String>();
                    try
                    {
                        UserService_.UpUserData(Int32.Parse(Us_Id), value, key);
                        ResponseContent.State = 1;
                        ResponseContent.Msg = "修改成功!";
                    }
                    catch (Exception ex)
                    {
                        ResponseContent.State = 0;
                        ResponseContent.Msg = ex.Message;
                    }
                    return Json(ResponseContent, JsonRequestBehavior.AllowGet);
                default:
                    return Json("错误接口", JsonRequestBehavior.AllowGet);
            }
        }
    }
}