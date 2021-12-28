using AutoMapper;
using DataAccess.Interfaces;
using MediatR;
using Models.Models;
using Web.UseCases.Flight.Dto;

namespace Web.UseCases.Flight.Commands
{
    public class UpdateFlightDelayCommand : IRequest<IResponse>
    {
        public UpdateFlightDelayDto UpdateFlightDelayDto { get; set; }
    }

    public class UpdateFlightDelayCommandHandler : IRequestHandler<UpdateFlightDelayCommand, IResponse>
    {
        private readonly IDbContext _dbContext;
        private readonly IMapper _mapper;

        public UpdateFlightDelayCommandHandler(IDbContext dbContext,
            IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<IResponse> Handle(UpdateFlightDelayCommand request, CancellationToken cancellationToken)
        {
            var flightFromDb = _dbContext.Flights.FirstOrDefault(x => x.Id == request.UpdateFlightDelayDto.Id);
            if (flightFromDb == null)
            {
                return new Response().SetStatusError().AddErrorMessage("Flight Not Found");
            }

            var updatedFlight = _mapper.Map<Models.Models.Flight>(request.UpdateFlightDelayDto);

            _mapper.Map(updatedFlight, flightFromDb);

            _dbContext.Flights.Update(updatedFlight);
            await _dbContext.SaveChangesAsync(cancellationToken);

            return new Response().SetStatusSuccess().AddSuccessMessage("Flight Updated");
        }
    }
}
