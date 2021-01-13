using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IBLL
{
    public interface ICommentBLL:IBaseService<bk_comments>
    {
        /// <summary>
        /// 获取评论详细信息->基本评论内容、作者信息
        /// </summary>
        /// <returns></returns>
        List<ResponseTComments> GetAllCommentDetailedInfo();

        /// <summary>
        /// 增加一条留言
        /// </summary>
        /// <param name="Content"></param>
        /// <param name="Author"></param>
        /// <returns></returns>
        Response<string> AddComments(string Content,string UserToken);

        /// <summary>
        /// 评论点赞
        /// </summary>
        /// <param name="Us_Id"></param>
        /// <returns></returns>
        Response<string> AddLikeNum(string UserToken, bk_comments CommentsInfo);
    }
}
