using Dominio.Contracts;
using Dominio.Implementation;
using Servicio.Contracts;
using Servicio.Implementation;

namespace BackendAPI.Configuration.Extensions
{
    public static class DependencyInjectionExtensions
    {
        public static void SetDependencyInjection(this IServiceCollection services)
        {
            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
            services.AddScoped<IUnitOfWork, UnitOfWork>();

            //Implemento un Servicio genérico
            services.AddScoped(typeof(IServicioGenerico<>), typeof(ServicioGenerico<>));

            //Registro los servicios
            services.AddScoped<IViajeService, ViajeService>();
            services.AddScoped<IVehiculoService, VehiculoService>();
            services.AddScoped<ITipoVehiculoService, TipoVehiculoService>();
            services.AddScoped<ICiudadService, CiudadService>();

        }

    }
}
