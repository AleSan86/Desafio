using Dominio.Contracts;

namespace Servicio.Contracts
{
    public interface IServicioGenerico<T> where T : IEntidad
    {
        T Get(short id);
        T Get(int id);
        T Get(double id);
        T Get(string id);
        Task<T> GetAsync(long id);
        IQueryable<T> GetAll();
        void Save(T entity);
        void Save(T entity, out int id);
        void Update(T entity);
        void Delete(int id);
    }
}
