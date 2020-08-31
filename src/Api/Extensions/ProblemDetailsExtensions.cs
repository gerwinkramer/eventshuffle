using System;
using Eventshuffle.Application.Exceptions;
using FluentValidation;
using Hellang.Middleware.ProblemDetails;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Eventshuffle.Api.Extensions
{
    public static class ProblemDetailsExtensions
    {
        public static IServiceCollection AddConfiguredProblemDetails(this IServiceCollection services)
        {
            services.AddProblemDetails(options =>
            {
                options.IncludeExceptionDetails = (context, ex) =>
                    context.RequestServices.GetRequiredService<IWebHostEnvironment>().IsDevelopment();

                options.MapToStatusCode<ValidationException>(StatusCodes.Status400BadRequest);
                options.MapToStatusCode<NotFoundException>(StatusCodes.Status404NotFound);
                options.MapToStatusCode<Exception>(StatusCodes.Status500InternalServerError);
            });

            return services;
        }
    }
}
