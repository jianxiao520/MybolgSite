using DAL;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Util;

namespace BLL
{
    public class UserBLL
    {
        // public static ArrayList User_Data = new ArrayList();
        /// <summary>
        /// 注册
        /// </summary>  
        /// <param name="Data">用户基础信息体</param>
        /// <returns></returns>
        public static bool Register(User Data)
        {
            //设置注册时间
            Data.Us_RegisterTime = DateTime.Now.ToLocalTime().ToString();
            //对密码使用MD5加密
            Data.Us_PassWord = MD5Helper.getMD5String(Data.Us_PassWord);

            //Data.Us_Ip = IpHelper.GetExternalIp();
            //置头像

            Data.Us_ProfilePhoto = "M_Tx.jpg";
            if (Data.Us_NickName == null)
                Data.Us_NickName = Data.Us_UserName;
            //交给数据访问层写入数据库
            new UserDAL().addUser(Data);
            //数据库操作
            return true;
        }

        /// <summary>
        /// 删除用户信息
        /// </summary>
        /// <param name="ID">User_ID</param>
        /// <returns></returns>
        public static bool ReMoveUserData(String ID)
        {
            new UserDAL().deleteUserById(ID);
            return true;
        }

        /// <summary>
        /// 更新多项字段 (自己)
        /// </summary>
        /// <param name="ID"></param>
        /// <param name="UserData"></param>
        /// <returns></returns>
        public static bool UpUserData(string ID, User UserData)
        {
            UserDAL Query = new UserDAL();
            int BoolInt = 0;
            foreach (var item in Query.QueryUser(UserData.Us_Eamil, "Us_Eamil"))
            {
                if (item.Us_Id != ID)
                    BoolInt++;
            }
            foreach (var item in Query.QueryUser(UserData.Us_Phone, "Us_Phone"))
            {
                if (item.Us_Id != ID)
                    BoolInt++;
            }
            if (BoolInt == 0)
            {
                if (UserData.Us_NickName != null && UserData.Us_NickName != "")
                    Query.UpUserData(ID, UserData.Us_NickName, "Us_NickName");
                if (UserData.Us_Phone != null && UserData.Us_Phone != "")
                    Query.UpUserData(ID, UserData.Us_Phone, "Us_Phone");
                if (UserData.Us_Eamil != null && UserData.Us_Eamil != "")
                    Query.UpUserData(ID, UserData.Us_Eamil, "Us_Eamil");
                if (UserData.Us_Birthday != null && UserData.Us_Birthday != "")
                    Query.UpUserData(ID, UserData.Us_Birthday, "Us_Birthday");
                if (UserData.Us_ProfilePhoto != null && UserData.Us_ProfilePhoto != "")
                    Query.UpUserData(ID, UserData.Us_ProfilePhoto, "Us_ProfilePhoto");
                if (UserData.Us_PassWord != null && UserData.Us_PassWord != "")
                    Query.UpUserData(ID, UserData.Us_PassWord, "Us_PassWord");
                return true;
            }
            return false;
        }


        /// <summary>
        /// 校验密码
        /// </summary>
        /// <param name="UserName"></param>
        /// <param name="PassWord"></param>
        /// <returns></returns>
        public static String Check_Pass(String UserName, String PassWord)
        {
            List<User> list = new List<User>();
            list = new UserDAL().QueryPwd(UserName, MD5Helper.getMD5String(PassWord));
            if (list.Count > 0)
            {
                if (UserName == "admin")
                {
                    return "0";
                }
                else
                {
                    return list[0].Us_Id;
                }
            }
            return "-1";
        }
        /// <summary>
        /// 查询用户整体数据
        /// </summary>
        /// <param name="ID">用户User_ID</param>
        /// <returns></returns>
        public static User FindUserData(String ID)
        {
            return new UserDAL().QueryUser(ID, "Us_Id")[0];
        }


        /// <summary>
        /// 查询所有用户信息
        /// </summary>
        /// <returns></returns>
        public static List<User> QueryAllData()
        {
            //这里可以判断一下是否为超级管理员身份
            //if(....)
            return new UserDAL().QueryAllData("admin");
        }


        public static string AddUserToken(string username,string LoginTime)
        {

            return "";
        }


    }
}
