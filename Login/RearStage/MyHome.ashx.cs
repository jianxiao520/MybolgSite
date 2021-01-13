using Bolg.BLL;
using Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Login
{
    /// <summary>
    /// MyHome 的摘要说明
    /// </summary>
    public class MyHome : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            if (context.Request.HttpMethod.ToLower() == "post")
            {
                bk_user SaveData = new bk_user();
                SaveData.Us_NickName = context.Request.Form["NickName"];
                SaveData.Us_Id = Int32.Parse(context.Request.Form["UsId"]);
                SaveData.Us_Eamil = context.Request.Form["Eamil"];
                SaveData.Us_Phone = context.Request.Form["Phone"];
                SaveData.Us_Birthday = Convert.ToDateTime(context.Request.Form["Birth"]);
                SaveData.Us_Sex = Int32.Parse(context.Request.Form["cars"]);
                SaveData.Us_ProfilePhoto = context.Request.Form["Img_url"];
                new  UserService().UpUserData(SaveData.Us_Id, SaveData);
            }
            context.Response.ContentType = "text/html";
            context.Response.Write(JsonConvert.SerializeObject(new Response<String> { State = 1, Msg = "修改成功" }));
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