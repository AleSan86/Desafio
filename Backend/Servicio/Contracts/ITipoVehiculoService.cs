using Dominio.Entidades;

namespace Servicio.Contracts
{
    public interface ITipoVehiculoService : IServicioGenerico<TipoVehiculo>
    {
        IQueryable<TipoVehiculo> GetAllTipos();
    }
}
