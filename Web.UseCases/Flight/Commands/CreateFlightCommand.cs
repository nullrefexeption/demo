using AutoMapper;
using DataAccess.Interfaces;
using MediatR;
using Models.Models;
using Web.UseCases.Flight.Dto;

namespace Web.UseCases.Flight.Commands
{
    public class CreateFlightCommand : IRequest<IResponse>
    {
        public CreateFlightDto CreateFlightDto { get; set; }
    }

    public class CreateFlightCommandHandler : IRequestHandler<CreateFlightCommand, IResponse>
    {
        private readonly IDbContext _dbContext;
        private readonly IMapper _mapper;

        public CreateFlightCommandHandler(IDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<IResponse> Handle(CreateFlightCommand request, CancellationToken cancellationToken)
        {
            var response = new Response<FlightDto>();

            var flight = _mapper.Map<Models.Models.Flight>(request.CreateFlightDto);
            _dbContext.Flights.Add(flight);
            await _dbContext.SaveChangesAsync(cancellationToken);

            response.Data = _mapper.Map<FlightDto>(flight);

            return response.AddSuccessMessage("Flight Created");
        }
    }
}
