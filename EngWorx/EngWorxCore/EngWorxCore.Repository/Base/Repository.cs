using RepositoryInterface;
using System;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;

namespace Repository
{
    public abstract class Repository<T> : ReadOnlyRepository<T>, IRepository<T> where T : class
    {
        protected virtual bool DeleteOnCascade(T entity)
        {
            return true;
        }

        public Repository(DbContext db)
            : base(db)
        {
        }

        public int Update(T entity)
        {
            DbEntityEntry<T> dbe = db.Entry<T>(entity);
            if (dbe.State == EntityState.Detached)
            {
                GetDbSet().Attach(entity);
                dbe.State = EntityState.Modified;
            }
            return 1;
        }

        public int InsertOnSubmit(T entity)
        {
            GetDbSet().Add(entity);
            return 1;
        }

        public int DeleteOnSubmit(T entity, bool deleteOnCascade = false)
        {
            int result = 0;
            if (!deleteOnCascade || DeleteOnCascade(entity))
            {
                GetDbSet().Remove(entity);
                result = 1;
            }
            else
            {
                result = -1;
                throw new Exception("Impossibile cancellare l'oggetto perchè esistono dei suoi riferimenti in Banca Dati.");
            }
            return result;
        }

        public int SaveChanges()
        {
            return db.SaveChanges();
        }
    }
}