using IDAL;
using System;
using System.Linq;
using System.Linq.Expressions;

namespace Bolg.BLL
{
    public abstract class BaseService<T> : IBLL.IBaseService<T> where T : class
    {
        public IBaseDAL<T> currentDal { get; set; }
        public IDbSession db { get; set; }

        /// <summary>
        /// 增
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public T Add(T entity)
        {
            T t = currentDal.Add(entity);
            return t;
        }

        /// <summary>
        /// 删
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public bool Delete(T entity)
        {
            bool result = currentDal.Delete(entity);

            return result;
        }

        /// <summary>
        /// 查
        /// </summary>
        /// <param name="whereLambda"></param>
        /// <returns></returns>
        public IQueryable<T> GetEntities(Expression<Func<T, bool>> whereLambda)
        {
            return currentDal.GetEntities(whereLambda);
        }

        /// <summary>
        /// 更
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public bool Update(T entity)
        {
            bool result = currentDal.Update(entity);
            return result;
        }


        /// <summary>
        /// 保存数据
        /// </summary>
        /// <returns></returns>
        public int SaveChanges()
        {
            return db.saveChanges();
        }
    }
}
