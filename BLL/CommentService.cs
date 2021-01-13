using IBLL;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bolg.BLL
{
    public class CommentService : BaseService<bk_comments>, ICommentBLL
    {
        /// <summary>
        /// 增加一条留言
        /// </summary>
        /// <param name="Content">评论内容</param>
        /// <param name="Author">作者</param>
        /// <returns></returns>     
        public Response<string> AddComments(string Content, string UserToken)
        {
            Response<string> Result = new Response<string>();
            bk_user UserInfo = Common.Cache.CacheHelper.GetCache<bk_user>(UserToken);
            if (UserInfo != null)
            {
                try
                {
                    db.CommentDAL.Add(new bk_comments
                    {
                        Comment_Content = Content,
                        Comment_Date = DateTime.Now,
                        Article_Id = 1,//评论文章ID，默认1
                        Comment_Like_Count = 0,//点赞
                        Comment_ParentId = 0,
                        Us_Id = UserInfo.Us_Id
                    });
                    Result.State = 1;
                    Result.Msg = "添加成功!~";
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

        /// <summary>
        /// 获取评论详细信息->基本评论内容、作者信息
        /// </summary>
        /// <returns></returns>
        public List<ResponseTComments> GetAllCommentDetailedInfo()
        {
            List<ResponseTComments> ResultInfo = new List<ResponseTComments>();
            List<bk_comments> CommentsInfo = db.CommentDAL.GetAllComments(10).ToList();
            List<bk_user> UserInfo = new List<bk_user>();
            foreach (var item in CommentsInfo)
            {
                UserInfo = db.UserDAL.QueryUser(item.Us_Id.ToString(), "Us_Id").ToList();
                ResultInfo.Add(new ResponseTComments
                {
                    Author = UserInfo.Count == 0 ? new bk_user { Us_ProfilePhoto = "M_Tx", Us_NickName = "用户已注销!" } : UserInfo[0],
                    Comment = item
                });
            }
            return ResultInfo;
        }

        /// <summary>
        /// 评论点赞
        /// </summary>
        /// <param name="UserToken"></param>
        /// <param name="CommentsInfo"></param>
        /// <returns></returns>
        public Response<string> AddLikeNum(string UserToken, bk_comments CommentsInfo)
        {
            Response<string> Result = new Response<string>();
            int From_UsId = Common.Cache.CacheHelper.GetCache<bk_user>(UserToken).Us_Id;
            if (From_UsId != CommentsInfo.Us_Id)
            {
                try
                {
                    db.CommentDAL.AddLikeCount(CommentsInfo,1);
                    Result.State = 1;
                    Result.Msg = "点赞成功!~";
                    db.saveChanges();
                }
                catch (Exception ex)
                {
                    Result.State = 0;
                    Result.Msg = "未知错误:" + ex.Message;
                }
            }
            else
            {
                Result.State = 0;
                Result.Msg = "登录过期，请重新登录后再操作!";
            }
            return Result;
        }

    }
}
