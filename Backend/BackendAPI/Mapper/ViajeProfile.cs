using AutoMapper;
using BackendAPI.Dto;
using Dominio.Entidades;

namespace BackendAPI.Mapper
{
    public class ViajeProfile : Profile
    {
        public ViajeProfile() 
        {
            CreateMap<Viaje, ViajeDto>();
            CreateMap<ViajeDto, Viaje>();
            CreateMap<Viaje, ViajeAMDto>();
            CreateMap<ViajeAMDto, Viaje>();
        }
    }
}
