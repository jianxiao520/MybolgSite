using IDAL;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bolg.Common;

namespace Bolg.DAL
{
    public class TokenDAL : BaseDAL<bk_usercookie>, ITokenDAL
    {
        /// <summary>
        /// 登录后生成token
        /// </summary>
        /// <param name="Uk">生成Token必要参数</param>
        public bk_usercookie AddLoginToken(int Us_Id)
        {
            bk_usercookie U_Cookie = new bk_usercookie();
            if (Us_Id > 0)
            {
                DateTime Date = DateTime.Now.AddDays(1);//Cookie有效期一天
                string Token = MD5Helper.getMD5String(Us_Id + Date.ToString());
                U_Cookie = new bk_usercookie { Us_Id = Us_Id, ExpirationDate = Date, Token = Token };
                //db.TokenDAL.AddLoginToken(U_Cookie);
            }
            this.Add(U_Cookie);
            return U_Cookie;
        }

        /// <summary>
        /// 删除重复登录的token
        /// </summary>
        /// <param name="Us_Id">用户ID</param>
        public void DelLoginToken(int Us_Id)
        {
            if (Us_Id > 0)
            {
                List<bk_usercookie> hp = this.GetEntities(o => o.Us_Id == Us_Id).ToList();
                foreach (var item in hp)
                    this.Delete(item);
            }
        }

        /// <summary>
        /// 查询Token有效期，没有返回空
        /// </summary>
        /// <param name="Token">提供Token</param>
        public string QueryToken(string Token)
        {
            string Data = "";
            if (Token != "")
                return Db.Set<bk_usercookie>().First(o => o.Token == Token).ExpirationDate.ToString();
            return Data;
        }
    }
}
