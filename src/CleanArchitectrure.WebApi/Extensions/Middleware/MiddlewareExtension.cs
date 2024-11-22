using System.Globalization;
using System.Text.Json.Serialization;
using System.Text.Json;

namespace CleanArchitectrure.WebApi.Extensions.Middleware
{
    public static class MiddlewareExtension
    {
        public static IApplicationBuilder AddMiddleware(this IApplicationBuilder app)
        {
            return app.UseMiddleware<ValidationMiddleware>();
        }
    }

    
}
