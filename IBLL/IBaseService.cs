using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace IBLL
{
    public interface IBaseService<T>
    {
        //我们考虑一下放什么？ 1、CRUD的方法 
        IQueryable<T> GetEntities(Expression<Func<T, bool>> whereLambda);
        //CUD
        T Add(T entity);
        bool Update(T entity);
        bool Delete(T entity);
        int SaveChanges();

    }
}
