using System.Diagnostics.CodeAnalysis;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using VerticalSlice.Application.Common.StackSpot;

namespace VerticalSlice.WebApi.DependencyInjection
{
    [ExcludeFromCodeCoverage]
    internal static class ApplicationBuilderExtensions
    {
        private const string Url = "/swagger/v1/swagger.json";
        private const string ApplicationName = "VerticalSlice.WebApi v1";

        internal static IApplicationBuilder UseApplicationServices(this IApplicationBuilder app,
            IConfiguration configuration,
            IWebHostEnvironment webHostEnvironment)
        {
            app.UseSwagger()
                .UseSwaggerUI(setup => setup.SwaggerEndpoint(Url, ApplicationName))
                .UseStackSpot(configuration, webHostEnvironment);

            return app;
        }
    }
}