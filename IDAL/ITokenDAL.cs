using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IDAL
{
    public interface ITokenDAL : IBaseDAL<bk_usercookie>
    {

        /// <summary>
        /// 登录后生成token
        /// </summary>
        /// <param name="Uk">生成Token必要参数</param>
        bk_usercookie AddLoginToken(int Us_Id);

        /// <summary>
        /// 查询Token有效期，没有返回空
        /// </summary>
        /// <param name="Token">提供Token</param>
        /// <returns></returns>
        string QueryToken(string Token);


        /// <summary>
        /// 删除重复登录的token
        /// </summary>
        /// <param name="Us_Id">用户ID</param>
        void DelLoginToken(int Us_Id);


    }
}
