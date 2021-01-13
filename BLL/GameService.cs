using IBLL;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bolg.BLL
{
    public class GameService : BaseService<bk_game>, IGameBLL
    {
        /// <summary>
        /// 查询排行榜数据
        /// </summary>
        /// <returns></returns>
        public List<UserScoreInfo> QueryRanking()
        {
            List<UserScoreInfo> ResultScoreInfo = new List<UserScoreInfo>();
            List<bk_game> ScoreInfo = db.GameDAL.GetAllScoreInfo(10).ToList();
            foreach (var item in ScoreInfo)
            {
                List<bk_user> UserInfo = db.UserDAL.QueryUser(item.Us_Id.ToString(), "Us_Id").ToList();
                if (UserInfo.Count() > 0)
                    ResultScoreInfo.Add(new UserScoreInfo { UserInfo = UserInfo[0], ScoreInfo = item });
            }

            return ResultScoreInfo;
        }

        /// <summary>
        /// 上传分数
        /// </summary>
        /// <param name="Us_Id"></param>
        /// <param name="Score"></param>
        /// <returns></returns>
        public Response<string> UpScore(string UserToken, int Score)
        {
            Response<string> Result = new Response<string>();
            bk_user UserInfo = Common.Cache.CacheHelper.GetCache<bk_user>(UserToken);
            if (UserInfo != null)
            {
                try
                {
                    if (db.GameDAL.GetEntities(o => o.Us_Id == UserInfo.Us_Id).ToList().Count() > 0)
                        db.GameDAL.Update(new bk_game
                        {
                            Us_Id = UserInfo.Us_Id,
                            Game_Score = Score
                        });
                    else
                        db.GameDAL.Add(new bk_game
                        {
                            Us_Id = UserInfo.Us_Id,
                            Game_Score = Score
                        });
                    Result.State = 1;
                    Result.Msg = "上传分数成功!~";
                    db.saveChanges();
                }
                catch (Exception ex)
                {
                    Result.State = 0;
                    Result.Msg = ex.Message;
                }
            }
            else
            {
                Result.State = 0;
                Result.Msg = "用户Token已过期，请重新登录!~";
            }
            return Result;
        }
    }
}
