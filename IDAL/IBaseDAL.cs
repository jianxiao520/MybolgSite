using System;
using System.Linq;
using System.Linq.Expressions;

namespace IDAL
{
    public interface IBaseDAL<T>
    {
        //查询 R
        IQueryable<T> GetEntities(Expression<Func<T, bool>> whereLambda);
        //加数据
        T Add(T entity);
        //更新数据
        bool Update(T entity);
        //删除数据
        bool Delete(T entity);
    }
}