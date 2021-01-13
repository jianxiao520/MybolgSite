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
    public partial class index : System.Web.UI.Page
    {
        public string ShowUserTrue = "";
        public string ShowUser = "<p class=\"iconfont\"></p>登录&注册";
        bk_user Uid = new bk_user();
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                string LoginToken = HttpContext.Current.Request.Cookies["Token"].Value;
                string LoginUid = HttpContext.Current.Request.Cookies["UsId"].Value;
                if (new TokenBLL().QueryToken(LoginToken) != null)
                {
                    bk_user Uid =new UserService().FindUserData(LoginUid);
                    ShowUser=string.Format("<p style=\"background: url('../UserHeadPortrait/{0}.jpg') no-repeat;background-size: 100% 100%;width: 35px;height: 35px;top: 4px;left: 5px;border-radius: 50%;display: inline-block;margin-right: 10px;position: absolute;\"></p>{1}", Uid.Us_ProfilePhoto, Uid.Us_NickName);
                    ShowUserTrue = "<div class=\"FlyMemu\"><a href=\"MyHome.aspx\">我的信息</a><a href=\"javascript: void(0); \" onclick=\"OutLogin()\">退出登录</a></div>";
                    
                }
            }
            catch
            {
                return;
            }
        }



    }
}