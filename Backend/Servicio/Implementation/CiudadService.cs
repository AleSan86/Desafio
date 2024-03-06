using Dominio.Contracts;
using Dominio.Entidades;
using Servicio.Contracts;

namespace Servicio.Implementation
{
    public class CiudadService : ServicioGenerico<Ciudad>, ICiudadService
    {
        private readonly IRepository<Ciudad> _repositorio;
        public CiudadService(IRepository<Ciudad> repositorio, IUnitOfWork unitOfWork) : base(repositorio, unitOfWork)
        {
            _repositorio = repositorio;
        }

        public IQueryable<Ciudad> GetAllCiudades()
        {
            return GetAll();
        }
        public async Task<Ciudad> GetById(int id)
        {
            return GetAll().FirstOrDefault(x => x.Id == id);
        }
        public async Task<Ciudad> DeleteCiudad(Ciudad ciudad)
        {
            ciudad.Activo = 0;
            _repositorio.Update(ciudad);
            return ciudad;
        }

    }
}
