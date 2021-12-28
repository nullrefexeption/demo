using AutoMapper;
using Web.UseCases.Flight.Dto;

namespace Web.UseCases.Flight.Mappings
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            CreateMap<Models.Models.Flight, FlightDto>();
            CreateMap<CreateFlightDto, Models.Models.Flight>();
            CreateMap<UpdateFlightDto, Models.Models.Flight>();
            CreateMap<UpdateFlightDelayDto, Models.Models.Flight>();
        }
    }
}
