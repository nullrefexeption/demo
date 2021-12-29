using System.Net;
using Web.UseCases;

namespace AirAstana.Web.Middleware
{
    public class ExceptionHandlerMiddleware
    {
        private readonly RequestDelegate _next;

        public ExceptionHandlerMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);
            }
            catch (EntityNotFoundException e)
            {
                httpContext.Response.StatusCode = (int)HttpStatusCode.NotFound;
            }
            catch (Exception e)
            {
                httpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            }
        }
    }

    public static class ExceptionHandlerMiddlewareExtensions
    {
        public static IApplicationBuilder UseExceptionHandlerMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<ExceptionHandlerMiddleware>();
        }
    }
}
