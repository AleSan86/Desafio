using AutoMapper;
using BackendAPI.Dto;
using Dominio.Entidades;

namespace BackendAPI.Mapper
{
    public class TipoVehiculoProfile : Profile
    {
        public TipoVehiculoProfile() 
        {
            CreateMap<TipoVehiculo, TipoVehiculoDto>();
            CreateMap<TipoVehiculoDto, TipoVehiculo>();
        }
    }
}
