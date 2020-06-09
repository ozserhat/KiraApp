using Framework.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Framework.Core.DataAccess
{
    public interface IEntityRepository<T> where T : class, IEntity, new()
    {
        List<T> GetList(Expression<Func<T, bool>> filter = null);
        T Get(Expression<Func<T, bool>> filter);
        T Add(T entity);
        List<T> Add(List<T> entity);

        T Update(T entity);

        List<T> Update(List<T> entity);
        void Delete(T entity);
    }
}
