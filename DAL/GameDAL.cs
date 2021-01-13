using IDAL;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bolg.DAL
{
    public class GameDAL:BaseDAL<bk_game>,IGameDAL
    {
        public IQueryable<bk_game> GetAllScoreInfo(int Num)
        {
            return this.GetEntities(o => o.Us_Id > 0).OrderByDescending(o => o.Game_Score).Take(Num);
        }
    }
}
