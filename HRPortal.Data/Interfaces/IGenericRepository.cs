using HRPortal.Model;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace HRPortal.Data
{
    public interface IGenericRepository<TEntity> where TEntity : class, IEntity
    {
        IEnumerable<TEntity> GetAll();

        IEnumerable<TEntity> GetAll(Expression<Func<TEntity, bool>> where);

        TEntity GetById(int id);

        void Update(TEntity entity);

        void Save(TEntity entity);

        void Delete(TEntity entity);
    }
}
