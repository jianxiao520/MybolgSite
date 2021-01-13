using IBLL;
using Model;
using Spring.Context;
using Spring.Context.Support;
using Spring.Objects.Factory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
namespace Bolg.UI.Portal.Controllers
{
    /// <summary>
    /// 每一个页面都需要判断是否已登录
    /// </summary>
    public class BaseController : Controller
    {
        public static bool UserIsLogin = false;
        public static bk_user Sso_UserInfo { get; set; }
        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            base.OnActionExecuting(filterContext);
            //确定为返回视图动作
            if (filterContext.ActionDescriptor.ActionName.IndexOf("_") == -1)
            {
                //已经登录处理事件
                if (Request.Cookies["UserToken"] != null)
                {
                    //判断是否已经登录缺进入登录页面
                    var AccessUrl = filterContext.HttpContext.Request.Url;
                    //从前端Cookie取得用户Token
                    string UserToken = Request.Cookies["UserToken"].Value;
                    //判断Token是否在高速缓存区
                    if (!(Common.Cache.CacheHelper.GetCache<bk_user>(UserToken) is bk_user UserInfo))
                    {
                        UserIsLogin = false;
                        //Token过期则清除全部Cookies
                        Response.Cookies["UserToken"].Expires = DateTime.Now.AddDays(-1);
                        ViewData["IsLogin"] = UserIsLogin == true ? "1" : "0";
                        return;
                    }
                    //设置全局登录
                    UserIsLogin = true;
                    //设置全局用户信息
                    Sso_UserInfo = UserInfo;
                    //窗口滑动工作，增加过期时间20分钟
                    Bolg.Common.Cache.CacheHelper.SetCache(UserToken, UserInfo, DateTime.Now.AddMinutes(20));
                }
                ViewData["IsLogin"] = UserIsLogin == true ? "1" : "0";

            }

        }
    }
}