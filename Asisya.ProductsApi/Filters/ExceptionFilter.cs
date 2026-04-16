using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Asisya.Api.Filters
{
    public class ExceptionFilter : IExceptionFilter
    {
        public void OnException(ExceptionContext context)
        {
            var exception = context.Exception;

            var response = exception switch
            {
                ArgumentException => new ObjectResult(new { message = exception.Message })
                {
                    StatusCode = StatusCodes.Status400BadRequest
                },

                UnauthorizedAccessException => new ObjectResult(new { message = exception.Message })
                {
                    StatusCode = StatusCodes.Status401Unauthorized
                },

                KeyNotFoundException => new ObjectResult(new { message = exception.Message })
                {
                    StatusCode = StatusCodes.Status404NotFound
                },

                _ => new ObjectResult(new
                {
                    message = "Error interno del servidor"
                })
                {
                    StatusCode = StatusCodes.Status500InternalServerError
                }
            };

            context.Result = response;
            context.ExceptionHandled = true;
        }
    }
}