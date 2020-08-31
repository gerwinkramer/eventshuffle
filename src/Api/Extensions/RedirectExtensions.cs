using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Rewrite;

namespace Eventshuffle.Api.Extensions
{
    public static class RedirectExtensions
    {
        public static IApplicationBuilder UseRedirects(this IApplicationBuilder app)
        {
            // Redirect root to Swagger
            var option = new RewriteOptions();
            option.AddRedirect("^$", "swagger");
            app.UseRewriter(option);

            return app;
        }
    }
}
