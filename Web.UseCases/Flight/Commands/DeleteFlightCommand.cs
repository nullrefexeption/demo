using DataAccess.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Web.UseCases.Flight.Dto;

namespace Web.UseCases.Flight.Commands
{
    public class DeleteFlightCommand : IRequest<Response>
    {
        public int Id { get; set; }
    }

    class DelayFlightDelayCommandHandler : IRequestHandler<DeleteFlightCommand, Response>
    {
        private readonly IDbContext _dbContext;

        public DelayFlightDelayCommandHandler(IDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Response> Handle(DeleteFlightCommand request, CancellationToken cancellationToken)
        {
            var flight = await _dbContext.Flights.FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken: cancellationToken);

            if (flight == null)
            {
                return new Response().SetStatusError().AddErrorMessage("Not Found");
            }

            _dbContext.Flights.Remove(flight);
            await _dbContext.SaveChangesAsync(cancellationToken);

            return new Response().SetStatusSuccess().AddSuccessMessage("Flight Deleted");
        }
    }
}
