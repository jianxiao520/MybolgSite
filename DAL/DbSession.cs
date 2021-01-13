using Bolg.DALFactory;
using IDAL;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bolg.DAL
{
    public class DbSession : IDbSession
    {
        public ITokenDAL TokenDAL { get; set; }
        public IUserDAL UserDAL { get; set; }
        public ICommentDAL CommentDAL { get; set; }
        public IGameDAL GameDAL { get; set; }
        public DbContext GetContext { get { return DbContextFactory.GetCurrentDbContext(); } }

        public int saveChanges()
        {
            return GetContext.SaveChanges();
        }
    }
}
