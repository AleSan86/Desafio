using Dominio.Contexto;
using Dominio.Contracts;
using Microsoft.EntityFrameworkCore;

namespace Dominio.Implementation
{
    public class Repository<T> : IRepository<T> where T : class, IEntidad
    {

        protected readonly DatabaseContext DbContext;

        public Repository(DatabaseContext context)
        {
            DbContext = context;
        }

        public void Delete(T entity)
        {
            if (DbContext.Entry(entity).State == EntityState.Detached)
                DbContext.Set<T>().Attach(entity);

            DbContext.Set<T>().Update(entity);
        }

        public void Delete(Func<T, bool> predicate)
        {
            var records = DbContext.Set<T>().Where(predicate);

            foreach (var record in records)
            {
                DbContext.Set<T>().Remove(record);
            }
        }

        public T Get(short id)
        {
            return DbContext.Set<T>().Find(id);
        }

        public T Get(int id)
        {
            return DbContext.Set<T>().Find(id);
        }

        public T Get(double id)
        {
            throw new NotImplementedException();
        }

        public T Get(string id)
        {
            throw new NotImplementedException();
        }

        public IQueryable<T> GetAll()
        {
            return DbContext.Set<T>();
        }

        public Task<T> GetAsync(long id)
        {
            throw new NotImplementedException();
        }

        public void Save(T entity)
        {
            DbContext.Set<T>().Add(entity);
        }

        public void Update(T entity)
        {
            DbContext.Set<T>().Update(entity);
        }

    }
}
