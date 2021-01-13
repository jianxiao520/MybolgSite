using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IBLL
{
    public interface ITokenBLL:IBaseService<bk_usercookie>
    {

        /// <summary>
        /// 加入新的Token
        /// </summary>
        /// <param name="Us_Id"></param>
        /// <returns></returns>
        bk_usercookie AddLoginToken(int Us_Id);


        /// <summary>
        /// 查询Token是否有效
        /// </summary>
        /// <param name="Token"></param>
        /// <returns></returns>
        string QueryToken(string Token);


        /// <summary>
        /// 删除Token
        /// </summary>
        /// <param name="Us_Id"></param>
        void DelLoginToken(int Us_Id);


    }
}
