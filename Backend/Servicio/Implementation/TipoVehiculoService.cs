using Dominio.Contracts;
using Dominio.Entidades;
using Servicio.Contracts;

namespace Servicio.Implementation
{
    public class TipoVehiculoService : ServicioGenerico<TipoVehiculo>, ITipoVehiculoService
    {
        public TipoVehiculoService(IRepository<TipoVehiculo> repositorio, IUnitOfWork unitOfWork) : base(repositorio, unitOfWork)
        {
        }

        public IQueryable<TipoVehiculo> GetAllTipos()
        {
            return GetAll();
        }
        public async Task<TipoVehiculo> GetByIdAsync(int id)
        {
            if (id != 0)
            {
                try
                {
                    return GetAll().FirstOrDefault(x => x.Id == id);
                }
                catch (Exception ex)
                {
                    throw new ArgumentNullException(ex.Message, ex);
                }
            }
            return null;
        }


    }
}
