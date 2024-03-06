using BackendAPI.Configuration.Models;

namespace BackendAPI.Configuration.Extensions
{
    public static class ConfigurationExtensions
    {
        public static GeneralConfiguration GetGeneralConfiguration(this IConfiguration configuration)
        {
            var sectionClientConfiguration = configuration.GetSection("GeneralConfiguration");
            return sectionClientConfiguration.Get<GeneralConfiguration>();
        }

        //Todo Crear clase estatica para el get del environment para consumir la API del clima
        //Todo Crear Modelo para la respuesta de la API
    }
}
