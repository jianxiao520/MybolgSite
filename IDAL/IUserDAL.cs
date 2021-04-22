using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IDAL
{
    public interface IUserDAL : IBaseDAL<bk_user>
    {

        /// <summary>
        /// 注册会员
        /// </summary>
        /// <param name="u"></param>
        void addUser(bk_user u);


        /// <summary>
        /// 字段条件查询用户信息
        /// </summary>
        /// <param name="Search_Value">字段内容</param>
        /// <param name="Search_Field">字段名称</param>
        /// <returns></returns>
        IQueryable<bk_user> QueryUser(string Search_Value, string Search_Field);


        /// <summary>
        /// 联合账号密码查询用户体
        /// </summary>
        /// <param name="UserName">用户名</param>
        /// <param name="PassWord">密码</param>
        /// <returns></returns>
        IQueryable<bk_user> QueryPwd(string UserName, string PassWord);


        /// <summary>
        /// 获取bk_User表全部信息
        /// </summary>
        /// <param name="AdminName">管理员账号名</param>
        /// <returns></returns>
        IQueryable<bk_user> QueryAllData(string AdminName);


        /// <summary>
        /// 更新字段信息
        /// </summary>
        /// <param name="User_Id">账号ID</param>
        /// <param name="Search_Value">字段内容</param>
        /// <param name="Search_Field">字段名称</param>
        void UpUserData(int User_Id, string Search_Value, string Search_Field);

        
        /// <summary>
        /// 根据用户ID更新给出的用户信息体
        /// </summary>
        /// <param name="User_Id">用户ID</param>
        /// <param name="u">用户信息体</param>
        void UpUserData(string User_Id, User u);


        /// <summary>
        /// 软删除用户ID
        /// </summary>
        /// <param name="User_Id">用户ID</param>
        void deleteUserById(string User_Id);
    }
}
