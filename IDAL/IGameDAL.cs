using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IDAL
{
    public interface IGameDAL:IBaseDAL<bk_game>
    {
        //bool AddScoure(int Us_Id,);
        IQueryable<bk_game> GetAllScoreInfo(int Num);
    }
}
