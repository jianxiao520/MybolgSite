using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IBLL
{
    public interface IUserBLL:IBaseService<bk_user>
    {
        /// <summary>
        /// 注册
        /// </summary>  
        /// <param name="Data">用户基础信息体</param>
        /// <returns></returns>
        bool Register(bk_user Data, string CodeToken);


        /// <summary>
        /// 删除用户信息
        /// </summary>
        /// <param name="ID">User_ID</param>
        /// <returns></returns>
        bool ReMoveUserData(String ID);

        /// <summary>
        /// 更新多项字段 (自己)
        /// </summary>
        /// <param name="ID"></param>
        /// <param name="UserData"></param>
        /// <returns></returns>
        bool UpUserData(int ID, bk_user UserData);


        /// <summary>
        /// 校验密码
        /// </summary>
        /// <param name="UserName"></param>
        /// <param name="PassWord"></param>
        /// <returns></returns>
        String Check_Pass(String UserName, String PassWord, string CodeToken, out bk_user ResultUserInfo);


        /// <summary>
        /// 查询用户整体数据
        /// </summary>
        /// <param name="ID">用户User_ID</param>
        /// <returns></returns>
        bk_user FindUserData(String ID);


        /// <summary>
        /// 查询所有用户信息
        /// </summary>
        /// <returns></returns>
        IQueryable<bk_user> QueryAllData(int page, int limit, out int AllNum);


        IQueryable<bk_user> QueryUserByUserName(string UserName, int page, int limit, out int AllNum);

        void UpUserData(int User_Id, string Search_Value, string Search_Field);
    }
}
