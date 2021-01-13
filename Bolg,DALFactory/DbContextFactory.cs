using Model;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;

namespace Bolg.DALFactory
{
    public class DbContextFactory
    {
        public static DbContext GetCurrentDbContext()
        {
            DbContext db = CallContext.GetData("DbContext") as DbContext;
            if (db == null)
            {
                db = new new_userEntities();
                CallContext.SetData("DbContext", db);
            }
            return db;
        }
    }

}
