using Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace Login
{
    /// <summary>
    /// UpHeadPortrait 的摘要说明
    /// </summary>
    public class UpHeadPortrait : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/html";
            HttpPostedFile file = context.Request.Files["file"];
            string ext = Path.GetExtension(file.FileName);
            if (!(ext.Equals(".jpg")))
            {
                context.Response.Write("shit");
                //中断请求
                context.Response.End();
            }
            else
            {
                //如果是图片，保存文件 确保文件的名字不重复 GUID编号
                string path = "/UserHeadPortrait/" + Guid.NewGuid().ToString() + Path.GetFileName(file.FileName);
                //保存文件到网站的Upload文件夹中
                file.SaveAs(context.Request.MapPath(path));
                ImgUpResponse rt = new ImgUpResponse();
                rt.ImgSize = file.ContentLength;
                rt.ImgUrl = path;
                string json = JsonConvert.SerializeObject(rt);
                context.Response.Write(json);
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