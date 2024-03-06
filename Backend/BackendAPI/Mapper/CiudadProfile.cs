using AutoMapper;
using BackendAPI.Dto;
using Dominio.Entidades;

namespace BackendAPI.Mapper
{
    public class CiudadProfile : Profile
    {
        public CiudadProfile() 
        {
            CreateMap<Ciudad, CiudadDto>();
            CreateMap<CiudadDto, Ciudad>();
            CreateMap<Ciudad, CiudadCrearDto>();
            CreateMap<CiudadCrearDto, Ciudad>();
        }
    }
}
