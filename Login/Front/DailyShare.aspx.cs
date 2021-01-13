using Bolg.BLL;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Login.Front
{
    public partial class DailyShare : System.Web.UI.Page
    {
        public string ShowUser = "<li><a href=\"Register.html\">登录&注册</a></li>";
        bk_user Uid = new bk_user();
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                string LoginToken = HttpContext.Current.Request.Cookies["Token"].Value;
                string LoginUid = HttpContext.Current.Request.Cookies["UsId"].Value;
                if (new TokenBLL().QueryToken(LoginToken) != null)
                {
                    bk_user Uid = new UserService().FindUserData(LoginUid);
                    ShowUser = string.Format("<li><a href=\"MyHome.aspx\" style=\"text-align:right;\"><p style=\"background: url('../UserHeadPortrait/{0}.jpg') no-repeat;background-size: 100% 100%;width: 33px;height: 33px;top:2px;left: 9px;border-radius: 50%;display: inline-block;margin-right: 10px;position: absolute;\"></p>{1}</a></li>", Uid.Us_ProfilePhoto, Uid.Us_NickName);
                }
            }
            catch
            {
                return;
            }
        }
    }
}