namespace Dominio.Contracts
{
    public interface IRepository<T> where T : IEntidad
    {
        T Get(short id);
        T Get(int id);
        T Get(double id);
        T Get(string id);
        Task<T> GetAsync(long id);
        IQueryable<T> GetAll();
        void Save(T entity);
        void Update(T entity);
        void Delete(T entity);
        void Delete(Func<T, bool> predicate);
    }
}
