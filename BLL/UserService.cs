
using Bolg.Common;
using IBLL;
using Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Bolg.BLL
{
    public class UserService : BaseService<bk_user>, IUserBLL
    {
        String appId = "c39a9fb7fce3ef53d325f5bdf1c7ba21";
        String appSecret = "e9791eddcd38fec67c191104950bb574";
        /// <summary>
        /// 校验密码
        /// </summary>
        /// <param name="UserName">用户名</param>
        /// <param name="PassWord">密码</param>
        /// <returns>返回JSON数据源</returns>
        public string Check_Pass(string UserName, string PassWord,string CodeToken, out bk_user ResultUserInfo)
        {
            string JSON = "";
            ResultUserInfo = null;
            List<bk_user> UserInfo = null;
            DxCsharpSDK.Captcha captcha = new DxCsharpSDK.Captcha(appId, appSecret);
            DxCsharpSDK.CaptchaResponse response = captcha.VerifyToken(CodeToken);
            //确保验证状态是SERVER_SUCCESS，SDK中有容错机制，在网络出现异常的情况会返回通过
            if (!response.result)
            {
                JSON = Json_Conver(new Response<String>
                {
                    State = -1,
                    Msg = "验证码异常!请重新滑动!"
                });
                return JSON;
            }
            UserInfo = db.UserDAL.QueryPwd(UserName, MD5Helper.getMD5String(PassWord)).ToList();
            if (UserInfo.Count != 0)
            {
                if (UserName == "admin")
                    JSON = Json_Conver(new Response<String>
                    {
                        State = 0,
                        Msg = "欢迎管理员登录~"
                    });
                else
                { 
                    JSON = Json_Conver(new Response<string>
                    {
                        State = 1,
                        Msg = MD5Helper.getMD5String(UserInfo[0].Us_Id + DateTime.Now.ToString())//加密Token
                    });
                    ResultUserInfo = UserInfo[0];
                }
            }
            else
                JSON = Json_Conver(new Response<String> {
                    State = -1,
                    Msg = "登录失败，请检查您的密码或账号名!"
                });
            return JSON;
        }

        /// <summary>
        /// 查询用户整体数据
        /// </summary>
        /// <param name="ID">用户User_ID</param>
        /// <returns></returns>
        public bk_user FindUserData(string ID)
        {
            bk_user u = new bk_user();
            u = db.UserDAL.QueryUser(ID, "Us_Id").ToArray()[0];
            return u;
        }

        /// <summary>
        /// 查询全部用户
        /// </summary>
        /// <returns></returns>
        public IQueryable<bk_user> QueryAllData()
        {
            //判断权限
            return db.UserDAL.QueryAllData("admin");
        }

        /// <summary>
        /// 注册
        /// </summary>
        /// <param name="Data">用户数据组</param>
        /// <returns></returns>
        public bool Register(bk_user Data, string CodeToken)
        {


            DxCsharpSDK.Captcha captcha = new DxCsharpSDK.Captcha(appId, appSecret);
            DxCsharpSDK.CaptchaResponse response = captcha.VerifyToken(CodeToken);
            //确保验证状态是SERVER_SUCCESS，SDK中有容错机制，在网络出现异常的情况会返回通过
            if (!response.result)
            {
                return false;
            }

            IQueryable<bk_user> Us;
            Us = db.UserDAL.QueryUser(Data.Us_UserName, "Us_UserName");
            if (Us.Count() >= 1)
                return false;
            //设置注册时间
            Data.Us_RegisterTime = DateTime.Now.ToLocalTime();
            //对密码使用MD5加密
            Data.Us_PassWord = MD5Helper.getMD5String(Data.Us_PassWord);
            //取Ip地址
            Data.Us_Ip = IPHelper.GetExternalIp();
            //置头像
            Data.Us_ProfilePhoto = "M_Tx";
            if (Data.Us_NickName == null)
                Data.Us_NickName = Data.Us_UserName;
            //交给数据访问层写入数据库
            db.UserDAL.addUser(Data);
            db.saveChanges();//一键确认数据
            return true;
        }

        /// <summary>
        /// 删除用户数据
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        public bool ReMoveUserData(string ID)
        {
            try
            {
                //递交给DAL层删除用户
                db.UserDAL.deleteUserById(ID);
                db.saveChanges();
            }
            catch (Exception)
            {
                return false;
            }
            return true;
        }

        /// <summary>
        /// 更新用户数据
        /// </summary>
        /// <param name="ID"></param>
        /// <param name="UserData"></param>
        /// <returns></returns>
        public bool UpUserData(int ID, bk_user UserData)
        {
                if (UserData.Us_NickName != null && UserData.Us_NickName != "")
                    db.UserDAL.UpUserData(ID, UserData.Us_NickName, "Us_NickName");
                if (UserData.Us_Phone != null && UserData.Us_Phone != "")
                {
                    //先判断其他用户有没有这个手机号
                    foreach (var item in db.UserDAL.QueryUser(UserData.Us_Phone, "Us_Phone"))
                        if (item.Us_Id != ID)
                            return false;
                    db.UserDAL.UpUserData(ID, UserData.Us_Phone, "Us_Phone");
                }
                if (UserData.Us_Eamil != null && UserData.Us_Eamil != "")
                {
                    //先判断其他用户有没有这个邮箱
                    foreach (var item in db.UserDAL.QueryUser(UserData.Us_Eamil, "Us_Eamil"))
                        if (item.Us_Id != ID)
                            return false;
                    db.UserDAL.UpUserData(ID, UserData.Us_Eamil, "Us_Eamil");
                }
                if (UserData.Us_Birthday != null && UserData.Us_Birthday.ToString() != "")
                    db.UserDAL.UpUserData(ID, UserData.Us_Birthday.ToString(), "Us_Birthday");
                if (UserData.Us_ProfilePhoto != null && UserData.Us_ProfilePhoto != "")
                    db.UserDAL.UpUserData(ID, UserData.Us_ProfilePhoto, "Us_ProfilePhoto");
                if (UserData.Us_PassWord != null && UserData.Us_PassWord != "")
                    db.UserDAL.UpUserData(ID, UserData.Us_PassWord, "Us_PassWord");
                if (UserData.Us_Sex >= -1 && UserData.Us_Sex < 1)
                    db.UserDAL.UpUserData(ID, UserData.Us_Sex.ToString(), "Us_Sex");
                db.saveChanges();
            return true;
        }

        /// <summary>
        /// 对象转换成JSON，方便内部使用
        /// </summary>
        /// <param name="FromObject"></param>
        /// <returns></returns>
        private string Json_Conver(object FromObject)
        {
            return JsonConvert.SerializeObject(FromObject);
        }
    }
}
