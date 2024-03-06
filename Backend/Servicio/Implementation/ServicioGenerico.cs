using Dominio.Contracts;
using Servicio.Contracts;

namespace Servicio.Implementation
{
    public class ServicioGenerico<T> : IServicioGenerico<T> where T : IEntidad
    {

        protected readonly IRepository<T> Repository;
        protected readonly IUnitOfWork UnitOfWork;

        public ServicioGenerico(IRepository<T> repositorio, IUnitOfWork unitOfWork)
        {
            Repository = repositorio;
            UnitOfWork = unitOfWork;
        }

        public T Get(short id)
        {
            return Repository.Get(id);
        }

        public T Get(int id)
        {
            return Repository.Get(id);
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
            return Repository.GetAll();
        }

        public Task<T> GetAsync(long id)
        {
            throw new NotImplementedException();
        }

        public virtual void Save(T entity)
        {
            try
            {
                Repository.Save(entity);
                UnitOfWork.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception("Ocurrio un error al guardar el registro", ex);
            }
        }
        public void Save(T entity, out int id)
        {
            Save(entity);
            id = (int)entity.GetType().GetProperty("Id").GetValue(entity);
        }

        public void Update(T entity)
        {
            try
            {
                Repository.Update(entity);
                UnitOfWork.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception("Ocurrio un error al actualizar el registro", ex);
            }
        }

        //public void Delete(T entity)
        //{
        //    Repository.Delete(entity);
        //    UnitOfWork.SaveChanges();
        //}

        public virtual void Delete(int id)
        {
            try
            {
                var item = Repository.Get(id);
                Repository.Delete(item);

                UnitOfWork.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception("Ocurrio un error al eliminar el registro", ex);
            }
        }
    }
}
