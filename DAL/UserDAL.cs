using Model;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class UserDAL
    {
        //数据库访问层中的要完成的对用户对象的操作

        /// <summary>
        /// 加入用户数据
        /// </summary>
        /// <param name="u">用户数据体</param>
        public void addUser(User u)
        {
            string sql = "insert into bk_user(Us_Ip,Us_UserName,Us_NickName,Us_PassWord,Us_Eamil,Us_ProfilePhoto,Us_RegisterTime,Us_Birthday,Us_Age,Us_Phone) values (?Us_Ip,?Us_UserName,?Us_NickName,?Us_PassWord,?Us_Eamil,?Us_ProfilePhoto,?Us_RegisterTime,?Us_Birthday,?Us_Age,?Us_Phone)";
            MySqlParameter[] p = new MySqlParameter[10];
            p[0] = new MySqlParameter("?Us_Ip", u.Us_Ip);
            p[1] = new MySqlParameter("?Us_UserName", u.Us_UserName);
            p[2] = new MySqlParameter("?Us_NickName", u.Us_NickName);
            p[3] = new MySqlParameter("?Us_PassWord", u.Us_PassWord);
            p[4] = new MySqlParameter("?Us_Eamil", u.Us_Eamil);
            p[5] = new MySqlParameter("?Us_ProfilePhoto", u.Us_ProfilePhoto);
            p[6] = new MySqlParameter("?Us_RegisterTime", u.Us_RegisterTime);
            p[7] = new MySqlParameter("?Us_Birthday", u.Us_Birthday);
            p[8] = new MySqlParameter("?Us_Age", u.Us_Age);
            p[9] = new MySqlParameter("?Us_Phone", u.Us_Phone);
            DAL.MySqlHelper.ExecuteSql(sql, p);
        }

        /// <summary>
        /// 字段条件查询用户信息
        /// </summary>
        /// <param name="Search_Value">字段内容</param>
        /// <param name="Search_Field">字段名称</param>
        /// <returns></returns>
        public List<User> QueryUser(string Search_Value, string Search_Field)
        {
            string Field = "";
            switch (Search_Field)
            {
                case "Us_Id":
                    Field = "Us_Id";
                    break;
                case "Us_UserName":
                    Field = "Us_UserName";
                    break;
                case "Us_Eamil":
                    Field = "Us_Eamil";
                    break;
                case "Us_Phone":
                    Field = "Us_Phone";
                    break;
                case "Us_NickName":
                    Field = "Us_NickName";
                    break;
                default:
                    Field = "Us_UserName";
                    break;
            }
            string sql = "select * from bk_user where " + Field + "=?Replace and Is_Del=0";
            MySqlParameter p = new MySqlParameter("?Replace", Search_Value);
            DataTable dt = DAL.MySqlHelper.QueryData(sql, p);
            List<User> list = new List<User>();
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                User u = new User();
                u.Us_Id = dt.Rows[i]["Us_Id"].ToString();
                u.Us_UserName = dt.Rows[i]["Us_UserName"].ToString();
                u.Us_NickName = dt.Rows[i]["Us_NickName"].ToString();
                u.Us_Eamil = dt.Rows[i]["Us_Eamil"].ToString();
                u.Us_ProfilePhoto = dt.Rows[i]["Us_ProfilePhoto"].ToString();
                u.Us_RegisterTime = dt.Rows[i]["Us_RegisterTime"].ToString();
                u.Us_Birthday = dt.Rows[i]["Us_Birthday"].ToString();
                u.Us_Age = int.Parse(dt.Rows[i]["Us_Age"].ToString());
                u.Us_Phone = dt.Rows[i]["Us_Phone"].ToString();
                list.Add(u);
            }
            return list;
        }


        /// <summary>
        /// 联合账号密码查询用户体
        /// </summary>
        /// <param name="UserName"></param>
        /// <param name="PassWord"></param>
        /// <returns></returns>
        public List<User> QueryPwd(string UserName, string PassWord)
        {
            string sql = "select * from bk_user where Us_UserName=?Us_UserName and Us_PassWord=?Us_PassWord and Is_Del=0";
            MySqlParameter[] p = new MySqlParameter[2];
            p[0] = new MySqlParameter("?Us_UserName", UserName);
            p[1] = new MySqlParameter("?Us_PassWord", PassWord);
            DataTable dt = DAL.MySqlHelper.QueryData(sql, p);
            List<User> list = new List<User>();
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                User u = new User();
                u.Us_Id = dt.Rows[i]["Us_Id"].ToString();
                u.Us_UserName = dt.Rows[i]["Us_UserName"].ToString();
                //u.Us_NickName = dt.Rows[i]["Us_NickName"].ToString();//用作考虑错误次数
                list.Add(u);
            }
            return list;
        }
        public List<User> QueryAllData(string AdminName)
        {
            string sql = "select * from bk_user where Us_UserName!=?admin and Is_Del=0";
            MySqlParameter p = new MySqlParameter("?admin", AdminName);
            DataTable dt = DAL.MySqlHelper.QueryData(sql, p);
            List<User> list = new List<User>();
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                User u = new User();
                u.Us_Id = dt.Rows[i]["Us_Id"].ToString();
                u.Us_UserName = dt.Rows[i]["Us_UserName"].ToString();
                u.Us_NickName = dt.Rows[i]["Us_NickName"].ToString();
                u.Us_PassWord = dt.Rows[i]["Us_PassWord"].ToString();
                u.Us_Eamil = dt.Rows[i]["Us_Eamil"].ToString();
                u.Us_Ip = dt.Rows[i]["Us_Ip"].ToString();
                u.Us_ProfilePhoto = dt.Rows[i]["Us_ProfilePhoto"].ToString();
                u.Us_RegisterTime = dt.Rows[i]["Us_RegisterTime"].ToString();
                u.Us_Birthday = dt.Rows[i]["Us_Birthday"].ToString();
                u.Us_Age = int.Parse(dt.Rows[i]["Us_Age"].ToString());
                u.Us_Phone = dt.Rows[i]["Us_Phone"].ToString();
                list.Add(u);
            }
            return list;
        }
        public void UpUserData(string User_Id, string Search_Value, string Search_Field)
        {
            string Field = "";
            switch (Search_Field)
            {
                case "Us_Ip":
                    Field = "Us_Ip";
                    break;
                case "Us_UserName":
                    Field = "Us_UserName";
                    break;
                case "Us_Eamil":
                    Field = "Us_Eamil";
                    break;
                case "Us_Phone":
                    Field = "Us_Phone";
                    break;
                case "Us_NickName":
                    Field = "Us_NickName";
                    break;
                case "Us_ProfilePhoto":
                    Field = "Us_ProfilePhoto";
                    break;
                case "Us_Birthday":
                    Field = "Us_Birthday";
                    break;
                case "Us_PassWord":
                    Field = "Us_PassWord";
                    break;
                default:
                    Field = "Us_UserName";
                    break;
            }
            string sql = "UPDATE bk_user set " + Field + "=?Replace where Us_Id=?Us_Id and Is_Del=0";
            MySqlParameter[] p = new MySqlParameter[2];
            p[0] = new MySqlParameter("?Us_Id", User_Id);
            p[1] = new MySqlParameter("?Replace", Search_Value);
            DAL.MySqlHelper.ExecuteSql(sql, p);
        }

        /// <summary>
        /// 根据用户ID更新给出的用户信息体
        /// </summary>
        /// <param name="User_Id">用户ID</param>
        /// <param name="u">用户信息体</param>
        public void UpUserData(string User_Id, User u)
        {
            string sql = "UPDATE bk_user set Us_NickName=?Us_NickName,Us_PassWord=?Us_PassWord,Us_Eamil=?Us_Eamil,Us_Phone=?Us_Phone where Us_Id=?Us_Id and Is_Del=0";
            MySqlParameter[] p = new MySqlParameter[5];
            p[0] = new MySqlParameter("?Us_Id", User_Id);
            p[1] = new MySqlParameter("?Us_NickName", u.Us_NickName);
            p[2] = new MySqlParameter("?Us_PassWord", u.Us_PassWord);
            p[3] = new MySqlParameter("?Us_Eamil", u.Us_Eamil);
            p[4] = new MySqlParameter("?Us_Phone", u.Us_Phone);
            DAL.MySqlHelper.ExecuteSql(sql, p);

        }
        /// <summary>
        /// 删除指定用户ID的
        /// </summary>
        /// <param name="User_Id">用户ID</param>
        public void deleteUserById(string User_Id)
        {
            //设置删除标识
            string sql = "UPDATE bk_user set Is_Del='1' where UserId=?UserId";
            MySqlParameter p = new MySqlParameter("?UserId", User_Id);
            DAL.MySqlHelper.ExecuteSql(sql, p);
        }

        /// <summary>
        /// 登录后生成token
        /// </summary>
        /// <param name="User_Id">用户ID</param>
        public void AddLoginToken(string User_Id)
        {
            string sql = "insert into bk_usercookie(Token,Us_Id,ExpirationDate) values (?Token,?Us_Id,?ExpirationDate)";
            MySqlParameter p = new MySqlParameter("?UserId", User_Id);
            DAL.MySqlHelper.ExecuteSql(sql, p);
        }

        /// <summary>
        /// 删除重复登录的token
        /// </summary>
        /// <param name="User_Id">用户ID</param>
        public void DelLoginToken(string User_Id)
        {
            string sql = "insert into bk_usercookie(Token,Us_Id,ExpirationDate) values (?Token,?Us_Id,?ExpirationDate)";
            MySqlParameter p = new MySqlParameter("?UserId", User_Id);
            DAL.MySqlHelper.ExecuteSql(sql, p);
        }
    }
}

