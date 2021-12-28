using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Models.Models;

namespace Web.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BaseApiController : ControllerBase
    {
        private IMediator _mediator;

        protected IMediator Mediator => _mediator ??= HttpContext.RequestServices.GetService<IMediator>();

        protected ActionResult HandleResult<T>(Response<T> result)
        {
            if (result == null)
            {
                return NotFound();
            }

            if (result.Status == Models.Enums.Status.Success && result.Data != null)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }
    }
}
