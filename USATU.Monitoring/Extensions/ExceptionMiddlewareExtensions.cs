using Microsoft.AspNetCore.Builder;
using USATU.Monitoring.Web.Middleware;

namespace USATU.Monitoring.Web.Extensions
{
    public static class ExceptionMiddlewareExtensions
    {
        public static void UseExceptionMiddleware(this IApplicationBuilder app)
        {
            app.UseMiddleware<ExceptionMiddleware>();
            app.UseMiddleware<ValidationExceptionMiddleware>();
            app.UseMiddleware<ObjectNotFoundExceptionMiddleware>();
        }

    }
}