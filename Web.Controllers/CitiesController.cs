using MediatR;
using Microsoft.AspNetCore.Mvc;
using Web.UseCases.City.Queries;

namespace Web.Controllers
{
    public class CitiesController : BaseApiController
    {
        private readonly ISender _sender;

        public CitiesController(ISender sender)
        {
            _sender = sender;
        }

        [HttpGet]
        public async Task<ActionResult> Get()
        {
            var result = await _sender.Send(new GetCitiesQuery());

            return Ok(result);
        }
    }
}
