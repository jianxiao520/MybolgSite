
using Bolg.BLL;
using Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Login.Front
{
    public partial class MyHome : System.Web.UI.Page
    {
        public string NickName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Birth { get; set; }
        public string Us_RegisterTime { get; set; }
        public string Us_Ip { get; set; }
        public string Us_ProfilePhoto { get; set; }
        public string[] Us_Sex { get; set; }
        public string LoginState { get; set; }
        protected void Page_Load(object sender, EventArgs e)
        {
            Us_Sex = new string[3] { "", "", "" };
            if (HttpContext.Current.Request.HttpMethod.ToLower() == "get")
            {
                try
                {
                    string LoginToken = HttpContext.Current.Request.Cookies["Token"].Value;
                    string LoginUid = HttpContext.Current.Request.Cookies["UsId"].Value;
                    if (new TokenBLL().QueryToken(LoginToken) != null)
                    {
                        bk_user NowUser = new UserService().FindUserData(LoginUid);
                        NickName = NowUser.Us_NickName;
                        Email = NowUser.Us_Eamil;
                        Phone = NowUser.Us_Phone;
                        Birth = NowUser.Us_Birthday.ToString();
                        Us_RegisterTime = NowUser.Us_RegisterTime.ToString();
                        Us_Ip = NowUser.Us_Ip;
                        
                        Us_Sex[Convert.ToInt32(NowUser.Us_Sex) + 1] = "selected";
                        Us_ProfilePhoto = "../UserHeadPortrait/" + NowUser.Us_ProfilePhoto+ ".jpg";
                        LoginState = "更改";
                    }
                    else
                    {
                        LoginState = "未登录";
                    }
                }
                catch
                {
                    LoginState = "未登录";
                    return;
                }
            }
            //else
            //{
            //    User SaveData = new User();
            //    SaveData.Us_Id = Int32.Parse(HttpContext.Current.Request.Form["UsId"]);
            //    SaveData.Us_NickName = HttpContext.Current.Request.Form["NickName"];
            //    SaveData.Us_Eamil = HttpContext.Current.Request.Form["Eamil"];
            //    SaveData.Us_Phone = HttpContext.Current.Request.Form["Phone"];
            //    SaveData.Us_Birthday = HttpContext.Current.Request.Form["Birth"];
            //    SaveData.Us_Sex = Int32.Parse(HttpContext.Current.Request.Form["cars"]);
            //    SaveData.Us_ProfilePhoto = HttpContext.Current.Request.Form["Img_url"];
            //    UserBLL.UpUserData(SaveData.Us_Id, SaveData);
            //    HttpContext.Current.Response.Write(JsonConvert.SerializeObject(new Response<String> { State = 1, Msg = "修改成功" }));
            //}
        }
    }
}