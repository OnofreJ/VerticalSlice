using System.Diagnostics.CodeAnalysis;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using VerticalSlice.Application.Common.StackSpot;
using VerticalSlice.Application.DependencyInjection;

namespace VerticalSlice.WebApi.DependencyInjection
{
    [ExcludeFromCodeCoverage]
    internal static class ServiceCollectionExtensions
    {
        private const string ApiVersionDescription = "v1";
        private const string GroupNameFormat = "'v'VVV";
        private const string AppicationName = "VerticalSlice.WebApi";

        internal static IServiceCollection InitializeApplicationServices(this IServiceCollection services,
            IConfiguration configuration, IWebHostEnvironment webHostEnvironment)
        {
            services.AddCustomVersioning()
                .AddUseCases(configuration)
                .AddStackSpot(configuration, webHostEnvironment)
                .AddSwagger();

            return services;
        }

        private static IServiceCollection AddSwagger(this IServiceCollection services)
        {
            services.AddSwaggerGen(setup =>
            {
                setup.SwaggerDoc(ApiVersionDescription,
                    new OpenApiInfo { Title = AppicationName, Version = ApiVersionDescription });
            });

            return services;
        }

        private static IServiceCollection AddCustomVersioning(this IServiceCollection services)
        {
            services.AddApiVersioning(options =>
            {
                options.ReportApiVersions = true;
                options.AssumeDefaultVersionWhenUnspecified = true;
                options.DefaultApiVersion = new ApiVersion(1, 0);
            })
            .AddVersionedApiExplorer(options =>
            {
                options.GroupNameFormat = GroupNameFormat;
                options.SubstituteApiVersionInUrl = true;
            });

            return services;
        }
    }
}