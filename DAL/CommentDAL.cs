using IDAL; 
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bolg.DAL
{
    public class CommentDAL : BaseDAL<bk_comments>, ICommentDAL
    {
        public bool AddComments(bk_comments CommentsInfo)
        {
            return this.Add(CommentsInfo) != null ? true: throw new Exception("添加失败");
        }

        public bool AddLikeCount(bk_comments CommentsInfo, int AddNum)
        {
            try
            {
                bk_comments Comments = this.GetEntities(o => o.Comment_Id == CommentsInfo.Comment_Id).ToList()[0];
                Comments.Comment_Like_Count += AddNum;
            }
            catch (Exception)
            {
                return false;
            }
            return true;
        }

        public IQueryable<bk_comments> GetAllComments(int Num)
        {
            return this.GetEntities(O=>O.Comment_Id>0).OrderByDescending(O=>O.Comment_Date).Take(Num); 
        }
    }
}
