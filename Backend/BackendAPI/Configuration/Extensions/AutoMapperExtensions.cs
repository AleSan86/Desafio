using AutoMapper;
using BackendAPI.Mapper;

namespace BackendAPI.Configuration.Extensions
{
    public static class AutoMapperExtensions
    {
        public static void AddAutoMapper(this IServiceCollection services)
        {
            services.AddSingleton(provider => new MapperConfiguration(mc =>
            {
                //Registro los perfiles
                mc.AddProfile(new ViajeProfile());
                mc.AddProfile(new VehiculoProfile());
                mc.AddProfile(new TipoVehiculoProfile());
                mc.AddProfile(new CiudadProfile());

            }).CreateMapper()
            );
        }
    }
}
