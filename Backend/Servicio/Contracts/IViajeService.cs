using Dominio.Entidades;
using Servicio.Implementation;

namespace Servicio.Contracts
{
    public interface IViajeService : IServicioGenerico<Viaje>
    {
        IQueryable<Viaje> GetAllViajes();
        Task<Viaje> GetById(int id);
        Task<Viaje> DeleteViaje(Viaje viaje);
    }
}
