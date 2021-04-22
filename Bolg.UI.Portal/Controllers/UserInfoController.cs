using Bolg.UI.Portal.Controllers;
using IBLL;
using Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Bolg.UI.Portal
{
    public class UserInfoController : BaseController
    {
        public IUserBLL UserService_ { get; set; }
        // GET: AlterProfilePhoto
        public ActionResult AlterProfilePhoto()
        {
            return View();
        }
        public ActionResult MyHome()
        {
            return View();
        }
        public ActionResult MyWealth()
        {
            return View();
        }
        public ActionResult UserInfo()
        {
            return View();
        }

        /// <summary>
        /// 处理图片上传
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ActionResult UpHeadPortrait_()
        {
            ImgUpResponse Result = new ImgUpResponse();
            try
            {
                HttpPostedFileBase file = Request.Files["file"];
                string ext = Path.GetExtension(file.FileName);
                if (!(ext.Equals(".jpg")))
                {
                    #region 图片格式不符合
                    Result.ImgSize = 0;
                    Result.ImgUrl = "";
                    Result.State = "必须为JPG格式文件";
                    #endregion

                }
                else
                {
                    #region 保存图片置本地
                    //如果是图片，保存文件 确保文件的名字不重复 GUID编号
                    var Gud = Guid.NewGuid().ToString();
                    string path = "../UserHeadPortrait/" + Gud + ".jpg";
                    //保存文件到网站的Upload文件夹中
                    file.SaveAs(Request.MapPath(path));
                    #endregion

                    #region 返回状态
                    Result.ImgSize = file.ContentLength;
                    Result.ImgUrl = Gud;
                    Result.State = "上传成功";
                    #endregion

                }
            }
            catch (Exception ee)
            {
                #region 出现异常
                Result.ImgSize = 0;
                Result.ImgUrl = "";
                Result.State = ee.Message;
                #endregion
            }
            return Json(Result);
        }

        /// <summary>
        /// 返回用户信息
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public JsonResult GetUserInfo_()
        {
            if (UserIsLogin != false)
                return Json(Sso_UserInfo);
            return Json("您未登录!");

        }

        /// <summary>
        /// 保存用户修改的数据
        /// </summary>
        /// <returns></returns>
        public ActionResult ModifUserInfo_(string UserToken, string NickName, string Eamil, string Phone, string Birth, string cars, string Img_url)
        {
            //判断是否是真的是自己
            string Msg = ""; int State = 0;
            bk_user UserData = Bolg.Common.Cache.CacheHelper.GetCache<bk_user>(UserToken);
            if (UserData != null)
            {
                try
                {
                    bk_user SaveData = new bk_user();
                    SaveData = new bk_user
                    {
                        Us_NickName = NickName,
                        Us_Id = UserData.Us_Id,
                        Us_Eamil = Eamil,
                        Us_Phone = Phone,
                        Us_Birthday = Birth != null ? Convert.ToDateTime(Birth) : new DateTime(),
                        Us_ProfilePhoto = Img_url,
                        Us_Sex = cars != null ? Int32.Parse(cars) : 0
                    };
                    if (UserService_.UpUserData(UserData.Us_Id, SaveData))
                    {
                        Msg = "保存成功"; State = 1;
                        //更新缓存数据
                        Bolg.Common.Cache.CacheHelper.SetCache(UserToken, UserService_.FindUserData(UserData.Us_Id.ToString()));
                    }
                    else
                    {
                        Msg = "保存失败：手机号或邮箱已存在，请修改！"; State = 0;
                    }
                }
                catch (Exception)
                {
                    Msg = "未知错误，保存失败~!"; State = 0;
                }
            }
            else
            {
                Msg = "身份信息过期~!"; State = 0;
            }
            return Json(new Response<string> { Msg = Msg, State = State });
        }
    }
}