using IDAL;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bolg.DAL
{
    public class UserDAL : BaseDAL<bk_user>, IUserDAL
    {

        /// <summary>
        /// 加入用户数据
        /// </summary>
        /// <param name="u">用户数据体</param>
        public void addUser(bk_user u)
        {
            this.Add(u);
        }

        /// <summary>
        /// 删除指定用户ID的
        /// </summary>
        /// <param name="User_Id">用户ID</param>
        public void deleteUserById(string User_Id)
        {
            //设置删除标识
            bk_user hp1 = Db.Set<bk_user>().First(o => o.Us_Id == Int32.Parse(User_Id));
            hp1.Is_Del = 1;
        }

        /// <summary>
        /// 查询数据表全部数据
        /// </summary>
        /// <param name="AdminName">管理员账号名</param>
        /// <returns></returns>
        public IQueryable<bk_user> QueryAllData(string AdminName)
        {
            return this.GetEntities(o => o.Us_UserName != AdminName).AsQueryable();
        }

        /// <summary>
        /// 联合账号密码查询用户体
        /// </summary>
        /// <param name="UserName">账号</param>
        /// <param name="PassWord">密码</param>
        /// <returns></returns>
        public IQueryable<bk_user> QueryPwd(string UserName, string PassWord)
        {   
            return this.GetEntities(o => o.Us_UserName.Equals(UserName)).Where(o => o.Us_PassWord.Equals(PassWord)).AsQueryable();
        }

        /// <summary>
        /// 字段条件查询用户信息
        /// </summary>
        /// <param name="Search_Value">字段内容</param>
        /// <param name="Search_Field">字段名称</param>
        /// <returns></returns>
        public IQueryable<bk_user> QueryUser(string Search_Value, string Search_Field)
        {
            IQueryable<bk_user> R_List;
            switch (Search_Field)
            {
                case "Us_Id":
                    int Us_IdInt = int.Parse(Search_Value);
                    R_List = this.GetEntities(o => o.Us_Id.Equals(Us_IdInt)).AsQueryable();
                    break;
                case "Us_UserName":
                    R_List = this.GetEntities(o => o.Us_UserName.Equals(Search_Value)).AsQueryable();
                    break;
                case "Us_Eamil":
                    R_List = this.GetEntities(o => o.Us_Eamil.Equals(Search_Value)).AsQueryable();
                    break;
                case "Us_Phone":
                    R_List = this.GetEntities(o => o.Us_Phone.Equals(Search_Value)).AsQueryable();
                    break;
                case "Us_NickName":
                    R_List = this.GetEntities(o => o.Us_NickName.Equals(Search_Value)).AsQueryable();
                    break;
                default:
                    R_List = this.GetEntities(o => o.Us_UserName.Equals(Search_Value)).AsQueryable();
                    break;
            }

            return R_List;
        }

        /// <summary>
        /// 指定用户ID，更新指定字段数据
        /// </summary>
        /// <param name="User_Id">用户信息</param>
        /// <param name="Search_Value">字段内容</param>
        /// <param name="Search_Field">字段名称</param>
        public void UpUserData(int User_Id, string Search_Value, string Search_Field)
        {
            bk_user hp1 = Db.Set<bk_user>().First(o => o.Us_Id == User_Id);
            switch (Search_Field)
            {
                case "Us_Ip":
                    hp1.Us_Ip = Search_Value;
                    break;
                case "Us_UserName":
                    hp1.Us_UserName = Search_Value;
                    break;
                case "Us_Eamil":
                    hp1.Us_Eamil = Search_Value;
                    break;
                case "Us_Phone":
                    hp1.Us_Phone = Search_Value;
                    break;
                case "Us_NickName":
                    hp1.Us_NickName = Search_Value;
                    break;
                case "Us_ProfilePhoto":
                    hp1.Us_ProfilePhoto = Search_Value;
                    break;
                case "Us_Birthday":
                    hp1.Us_Birthday = Convert.ToDateTime(Search_Value);
                    break;
                case "Us_PassWord":
                    hp1.Us_PassWord = Search_Value;
                    break;
                case "Us_Sex":
                    hp1.Us_Sex = Int32.Parse(Search_Value);
                    break;
                default:
                    hp1.Us_UserName = Search_Value;
                    break;
            }
            //XusContext.SaveChanges();
        }

        /// <summary>
        /// 根据用户ID更新给出的用户信息体
        /// </summary>
        /// <param name="User_Id">用户ID</param>
        /// <param name="u">用户信息体</param>
        public void UpUserData(string User_Id, User u)
        {
            bk_user hp1 = Db.Set<bk_user>().First(o => o.Us_Id == Int32.Parse(User_Id));
            hp1.Us_NickName = u.Us_NickName;
            hp1.Us_PassWord = u.Us_PassWord;
            hp1.Us_Eamil = u.Us_Eamil;
            hp1.Us_Phone = u.Us_Phone;
            // XusContext.SaveChanges();
        }
    }
}
