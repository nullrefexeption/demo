using AutoMapper;
using Web.UseCases.City.Dto;

namespace Web.UseCases.City.Mappings
{
    public class CityMapperProfile : Profile
    {
        public CityMapperProfile()
        {
            CreateMap<Models.Models.City, CityDto>();
        }
    }
}
