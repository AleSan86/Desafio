using AutoMapper;
using BackendAPI.Dto;
using Dominio.Entidades;

namespace BackendAPI.Mapper
{
    public class VehiculoProfile : Profile
    {
        public VehiculoProfile()
        {
            CreateMap<Vehiculo, VehiculoDto>();
            CreateMap<VehiculoDto, Vehiculo>();
            CreateMap<TipoVehiculoDto, TipoVehiculo>();
            CreateMap<TipoVehiculo, TipoVehiculoDto>();
            CreateMap<VehiculoAltaDto, Vehiculo>();
            CreateMap<Vehiculo, VehiculoAltaDto>();
            CreateMap<VehiculoEditarDto, Vehiculo>();
            CreateMap<Vehiculo, VehiculoEditarDto>();
        }
    }
}
