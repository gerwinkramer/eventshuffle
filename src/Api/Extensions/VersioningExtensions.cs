using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;

namespace Eventshuffle.Api.Extensions
{
    public static class VersioningExtensions
    {
        public static IServiceCollection AddConfiguredApiVersioning(this IServiceCollection services)
        {
            services.AddApiVersioning(options => options.ReportApiVersions = true);
            services.AddVersionedApiExplorer(options =>
            {
                options.GroupNameFormat = "'v'VVV";
                options.SubstituteApiVersionInUrl = true;
                options.DefaultApiVersion = new ApiVersion(1, 0);
                options.AssumeDefaultVersionWhenUnspecified = true;
            });

            return services;
        }
    }
}
