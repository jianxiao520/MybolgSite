using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IDAL
{
    public interface ICommentDAL:IBaseDAL<bk_comments>
    {
        /// <summary>
        /// 获取全部评论数据
        /// </summary>
        /// <returns></returns>
        IQueryable<bk_comments> GetAllComments(int Num);

        bool AddComments(bk_comments CommentsInfo);

        bool AddLikeCount(bk_comments CommentsInfo, int AddNum);
    }
}
