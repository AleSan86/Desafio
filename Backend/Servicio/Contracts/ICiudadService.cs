using Dominio.Entidades;

namespace Servicio.Contracts
{
    public interface ICiudadService : IServicioGenerico<Ciudad>
    {
        IQueryable<Ciudad> GetAllCiudades();
        Task<Ciudad> GetById(int id);
        Task<Ciudad> DeleteCiudad(Ciudad ciudad);
    }
}
