using System;
using System.IO;
using System.Reflection;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;

namespace Eventshuffle.Api.Extensions
{
    public static class SwaggerExtensions
    {
        public static IServiceCollection AddConfiguredSwagger(this IServiceCollection services)
        {
            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "Eventshuffle API",
                    Description = "Eventshuffle API. Plan ahead!"
                });

                // Map NodaTime LocalDate to an OpenAPI date.
                options.MapType<NodaTime.LocalDate>(() => new OpenApiSchema { Type = "string", Format = "date" });

                // Set the comments path for the Swagger JSON and UI.
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                options.IncludeXmlComments(xmlPath);
            });

            return services;
        }

        public static IApplicationBuilder UseConfiguredSwagger(this IApplicationBuilder app)
        {
            app.UseSwagger();
            app.UseSwaggerUI(options =>
            {
                options.SwaggerEndpoint("/swagger/v1/swagger.json", "Eventshuffle API v1");
            });

            return app;
        }
    }
}
