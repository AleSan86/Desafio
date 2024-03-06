using Dominio.Contexto;
using Dominio.Contracts;

namespace Dominio.Implementation
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly DatabaseContext _contexto;
        public UnitOfWork(DatabaseContext contexto)
        {
            _contexto = contexto;
        }
        public int SaveChanges()
        {
            return _contexto.SaveChanges();
        }

    }
}
