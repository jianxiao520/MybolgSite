using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IBLL
{
    public interface IGameBLL:IBaseService<bk_game>
    {
        /// <summary>
        /// 上传分数
        /// </summary>
        /// <param name="Us_Id"></param>
        /// <param name="Score"></param>
        /// <returns></returns>
        Response<string> UpScore(string UserToken, int Score);

        /// <summary>
        /// 查询排行榜数据
        /// </summary>
        /// <returns></returns>
        List<UserScoreInfo> QueryRanking();
    }
}
