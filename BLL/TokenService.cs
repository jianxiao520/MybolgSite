using IBLL;
using IDAL;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bolg.BLL
{
    public class TokenService : BaseService<bk_usercookie>, ITokenBLL
    {
        public bk_usercookie AddLoginToken(int Us_Id)
        {
            return db.TokenDAL.AddLoginToken(Us_Id);
        }

        public void DelLoginToken(int Us_Id)
        {
            db.TokenDAL.DelLoginToken(Us_Id);
            db.saveChanges();
        }

        public string QueryToken(string Token)
        {
            return db.TokenDAL.QueryToken(Token);
        }
    }
}
