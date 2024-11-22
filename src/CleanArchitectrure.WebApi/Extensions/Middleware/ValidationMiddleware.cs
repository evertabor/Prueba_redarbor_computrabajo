using CleanArchitectrure.Application.UseCases.Commons.Bases;
using CleanArchitectrure.Application.UseCases.Commons.Exceptions;
using System.Net;
using System.Text.Json;

namespace CleanArchitectrure.WebApi.Extensions.Middleware
{
    public class ValidationMiddleware
    {
        private readonly RequestDelegate _next;

        public ValidationMiddleware(RequestDelegate next)
        {
            _next = next ?? throw new ArgumentNullException(nameof(next));
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next.Invoke(context);
            }
            catch (ValidationExceptionCustom ex)
            {
                await HandleExceptionAsync(context, ex, (int)HttpStatusCode.BadRequest);
            }
            catch (NotFoundExceptionCustom ex)
            {
                await HandleExceptionAsync(context, ex, (int)HttpStatusCode.NotFound);
            }
            catch (Exception ex) 
            {
                await HandleExceptionAsync(context, ex, (int)HttpStatusCode.InternalServerError);
            }
        }

        private static Task HandleExceptionAsync(HttpContext context, Exception exception, int statusCode)
        {
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = statusCode;

            var response = new BaseResponse<object>
            {
                Message = exception is ValidationExceptionCustom validationException ? "Validation Errors" : exception.Message,
                Errors = exception is ValidationExceptionCustom validationEx ? validationEx.Errors.ToList() : null,
                Data = null,
                succcess = false
            };

            return context.Response.WriteAsync(JsonSerializer.Serialize(response));
        }
    }
}
