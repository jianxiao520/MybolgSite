using IDAL;
using Spring.Context;
using Spring.Context.Support;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;

namespace Bolg.DALFactory
{
    public class DbSessionFactory
    {
        public static IDbSession GetDbSession()
        {
            IDbSession db = CallContext.GetData("DbSession") as IDbSession;
            if (db == null)
            {
                IApplicationContext ctx = ContextRegistry.GetContext();
                db = ctx.GetObject("DbSession") as IDbSession;
                CallContext.SetData("DbSession", db);
            }
            return db;
        }
    }
}
