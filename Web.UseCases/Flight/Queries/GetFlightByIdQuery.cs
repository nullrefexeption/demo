using MediatR;
using Models.Models;
using Web.UseCases.Flight.Dto;

namespace Web.UseCases.Flight.Queries
{
    public class GetFlightByIdQuery : IRequest<FlightDto>
    {
        public int Id { get; set; }
    }
}
