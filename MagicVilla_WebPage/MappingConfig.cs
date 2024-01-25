using AutoMapper;
using MagicVilla_WebPage.Models.Dto;

namespace MagicVilla_WebPage
{
    public class MappingConfig : Profile
    {
        public MappingConfig()
        {
            CreateMap<VillaDTO, CreateVillaDTO>().ReverseMap();
            CreateMap<VillaDTO, UpdateVillaDTO>().ReverseMap();

            CreateMap<VillaNumberDTO, VillaNumberCreateDTO>().ReverseMap();
            CreateMap<VillaNumberDTO, VillaNumberUpdateDTO>().ReverseMap();

        }
    }
}
