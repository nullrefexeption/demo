using AutoMapper;
using DataAccess.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Web.UseCases.Flight.Dto;

namespace Web.UseCases.Flight.Queries
{
    public class GetFlightByIdQuery : IRequest<FlightDto>
    {
        public int Id { get; set; }
    }

    public class GetFlightByIdQueryHandler : IRequestHandler<GetFlightByIdQuery, FlightDto>
    {
        private readonly IMapper _mapper;
        private readonly IDbContext _dbContext;

        public GetFlightByIdQueryHandler(
            IMapper mapper,
            IDbContext dbContext
            )
        {
            _mapper = mapper;
            _dbContext = dbContext;
        }

        public async Task<FlightDto> Handle(GetFlightByIdQuery request, CancellationToken cancellationToken)
        {
            var flight = await _dbContext.Flights
                .Include(x => x.ArrivalCity)
                .Include(x => x.DepartureCity)
                .FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken: cancellationToken);

            if (flight == null)
            {
                throw new EntityNotFoundException();
            }

            var flightDto  = _mapper.Map<FlightDto>(flight);

            return flightDto;
        }
    }
}
