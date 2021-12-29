using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Models.Models;
using Models.Models.Table;
using Web.UseCases.Flight.Commands;
using Web.UseCases.Flight.Dto;
using Web.UseCases.Flight.Queries;

namespace Web.Controllers
{
    public class FlightsController : BaseApiController
    {
        private readonly ISender _sender;

        public FlightsController(ISender sender)
        {
            _sender = sender;
        }

        [HttpPost("list")]
        public async Task<ActionResult<TableData<FlightDto>>> GetAll(TableParams? parameters)
        {
            var result = await _sender.Send(new GetFlightsQuery() { Params = parameters});

            return Ok(result);
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<FlightDto>> GetById(int id)
        {
            var result = await _sender.Send(new GetFlightByIdQuery { Id = id });

            return Ok(result);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<Response<FlightDto>>> CreateFlight(CreateFlightDto createFlightDto)
        {
            var result = await _sender.Send(new CreateFlightCommand() { CreateFlightDto = createFlightDto });
            return Ok(result);
        }

        [HttpPut]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<Response>> UpdateFlight(UpdateFlightDto updateFlightDto)
        {
            var result = await _sender.Send(new UpdateFlightCommand() { UpdateFlightDto = updateFlightDto });
            return Ok(result);
        }

        [HttpPost("changeDelay")]
        [Authorize(Roles = "Admin, Moderator")]
        public async Task<ActionResult<Response>> ChangeFlightDelay(UpdateFlightDelayDto updateFlightDelayDto)
        {
            var result = await _sender.Send(new UpdateFlightDelayCommand() { UpdateFlightDelayDto = updateFlightDelayDto });
            return Ok(result);
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteFlight(int id)
        {
            var result = await _sender.Send(new DeleteFlightCommand() { Id = id });
            return Ok(result);
        }
    }
}
