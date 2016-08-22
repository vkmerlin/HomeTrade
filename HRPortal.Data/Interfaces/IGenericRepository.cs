using HRPortal.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace HRPortal.Data
{
    public interface IGenericRepository<TEntity> where TEntity : class, IEntity
    {
        List<TEntity> GetAll();

        List<TEntity> GetAll(Expression<Func<TEntity, bool>> where);

        TEntity GetById(int id);

        void Update(TEntity entity);

        void Save(TEntity entity);

        void Delete(TEntity entity);
    }
}
