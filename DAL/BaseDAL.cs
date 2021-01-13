using Bolg.DALFactory;
using IDAL;
using Model;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Bolg.DAL
{
    public class BaseDAL<T> : IBaseDAL<T> where T : class
    {
        public DbContext Db { get { return DbContextFactory.GetCurrentDbContext(); } }

        /// <summary>
        /// 数据库信息[增加]
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public T Add(T entity)
        {
            T t = Db.Set<T>().Add(entity);
            return t;
        }

        /// <summary>
        /// 数据库信息[删除]
        /// </summary>
        /// <param name="entity">语句</param>
        /// <returns></returns>
        public bool Delete(T entity)
        {
            T t = Db.Set<T>().Attach(entity);
            Db.Entry(entity).State = EntityState.Deleted;
            return true;
        }


        /// <summary>
        /// 数据库信息[查询]
        /// </summary>
        /// <param name="whereLambda">语句</param>
        /// <returns></returns>
        public IQueryable<T> GetEntities(Expression<Func<T, bool>> whereLambda)
        {
            return Db.Set<T>().AsNoTracking().Where(whereLambda).AsQueryable();
        }

        /// <summary>
        /// 数据库信息[更新]
        /// </summary>
        /// <param name="entity">语句</param>
        /// <returns></returns>
        public bool Update(T entity)
        {
            T t = Db.Set<T>().Attach(entity);
            Db.Entry(entity).State = EntityState.Modified;
            return true;
        }

        public int SaveChanges()
        {
            return Db.SaveChanges();
        }
    }
}
