using HRPortal.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;

namespace HRPortal.Data
{
    public abstract class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : class, IEntity
    {
        HRPortalContext db;
        public GenericRepository()
        {
            db = new HRPortalContext();
        }

        public IEnumerable<TEntity> GetAll()
        {
            return db.Set<TEntity>().ToList();
        }

        public IEnumerable<TEntity> GetAll(Expression<Func<TEntity, bool>> where)
        {
            return db.Set<TEntity>().Where(where).ToList();
        }

        public TEntity GetById(int id)
        {
            return db.Set<TEntity>().SingleOrDefault(x => x.Id == id);
        }

        public void Update(TEntity entity)
        {
            db.Entry(entity).State = EntityState.Modified;
            db.SaveChanges();
        }

        public void Save(TEntity entity)
        {
            db.Set<TEntity>().Add(entity);
            db.SaveChanges();
        }

        public void Delete(TEntity entity)
        {
            if (db.Entry(entity).State == EntityState.Detached)
                db.Set<TEntity>().Attach(entity);
            db.Set<TEntity>().Remove(entity);
            db.SaveChanges();
        }
    }
}
