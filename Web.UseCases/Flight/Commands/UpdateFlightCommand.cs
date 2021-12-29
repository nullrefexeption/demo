using AutoMapper;
using DataAccess.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Models.Models;
using Web.UseCases.Flight.Dto;

namespace Web.UseCases.Flight.Commands
{
    public class UpdateFlightCommand : IRequest<IResponse>
    {
        public UpdateFlightDto UpdateFlightDto { get; set; }
    }

    class UpdateFlightCommandHandler : IRequestHandler<UpdateFlightCommand, IResponse>
    {
        private readonly IDbContext _dbContext;
        private readonly IMapper _mapper;

        public UpdateFlightCommandHandler(IDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<IResponse> Handle(UpdateFlightCommand request, CancellationToken cancellationToken)
        {
            var flightFromDb = await _dbContext.Flights.FirstOrDefaultAsync(x => x.Id == request.UpdateFlightDto.Id, cancellationToken: cancellationToken);
            if (flightFromDb == null)
            {
               return new Response().SetStatusError().AddErrorMessage("Flight Not Found");
            }

            _mapper.Map(request.UpdateFlightDto, flightFromDb);

            _dbContext.Flights.Update(flightFromDb);
            await _dbContext.SaveChangesAsync(cancellationToken);

            return new Response().SetStatusSuccess().AddSuccessMessage("Flight Updated");
        }
    }
}
