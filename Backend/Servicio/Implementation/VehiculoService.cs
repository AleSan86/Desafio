using Dominio.Contracts;
using Dominio.Entidades;
using Servicio.Contracts;

namespace Servicio.Implementation
{
    public class VehiculoService : ServicioGenerico<Vehiculo>, IVehiculoService
    {
        private readonly IRepository<Vehiculo> _repositorio;

        public VehiculoService(IRepository<Vehiculo> repositorio, IUnitOfWork unitOfWork) : base(repositorio, unitOfWork)
        {
            _repositorio = repositorio;
        }

        public IQueryable<Vehiculo> GetAllVehiculos()
        {
            return GetAll();
        }
        public async Task<Vehiculo?> GetById(int id)
        {
            return GetAll().FirstOrDefault(x => x.Id == id);
        }
        public async Task<Vehiculo> DeleteVehiculo(Vehiculo vehiculo)
        {
            vehiculo.Activo = 0;
            _repositorio.Update(vehiculo);
            return vehiculo;
        }

    }
}
