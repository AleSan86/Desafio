using Dominio.Contracts;
using Dominio.Entidades;
using Servicio.Contracts;

namespace Servicio.Implementation
{
    public class ViajeService : ServicioGenerico<Viaje>, IViajeService
    {
        private readonly IRepository<Viaje> _repositorio;
        public ViajeService(IRepository<Viaje> repositorio, IUnitOfWork unitOfWork) : base(repositorio, unitOfWork)
        {
            _repositorio = repositorio;
        }

        public IQueryable<Viaje> GetAllViajes()
        {
            return GetAll();
        }
        public async Task<Viaje?> GetById(int id)
        {
            return GetAll().FirstOrDefault(x => x.Id == id);
        }
        public async Task<Viaje> DeleteViaje(Viaje viaje)
        {
            viaje.Activo = 0;
            _repositorio.Update(viaje);
            return viaje;
        }

    }
}
