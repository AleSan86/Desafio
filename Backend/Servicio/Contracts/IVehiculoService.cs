using Dominio.Entidades;

namespace Servicio.Contracts
{
    public interface IVehiculoService : IServicioGenerico<Vehiculo>
    {
        IQueryable<Vehiculo> GetAllVehiculos();
        Task<Vehiculo> GetById(int id);
        Task<Vehiculo> DeleteVehiculo(Vehiculo vehiculo);
    }
}
