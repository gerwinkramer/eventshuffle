using Eventshuffle.Api.Extensions;
using Eventshuffle.Application.Behaviours;
using Eventshuffle.Application.Services;
using FluentValidation;
using Hellang.Middleware.ProblemDetails;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NodaTime;
using NodaTime.Serialization.SystemTextJson;

namespace Eventshuffle.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddConfiguredProblemDetails();
            services.AddControllers()
                .AddJsonOptions(options =>
                {
                    var settings = options.JsonSerializerOptions;
                    settings.ConfigureForNodaTime(DateTimeZoneProviders.Tzdb);
                });
            services.AddRouting(options => options.LowercaseUrls = true);
            services.AddConfiguredApiVersioning();
            services.AddConfiguredSwagger();
            services.AddMediatR(typeof(IEventshuffleDbContext).Assembly);
            services.AddValidatorsFromAssembly(typeof(IEventshuffleDbContext).Assembly);
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehaviour<,>));
            services.AddPersistence(Configuration);
        }

        public static void Configure(IApplicationBuilder app)
        {
            app.UseProblemDetails();
            app.UseRouting();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            app.UseConfiguredSwagger();
            app.UseRedirects();
        }
    }
}
