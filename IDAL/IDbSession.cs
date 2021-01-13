using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IDAL
{
    public interface IDbSession
    {
        ITokenDAL TokenDAL { get; }
        IUserDAL UserDAL { get; }
        ICommentDAL CommentDAL { get; }
        IGameDAL GameDAL { get; }
        int saveChanges();
    }
}
