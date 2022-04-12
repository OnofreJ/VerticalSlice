using System.Diagnostics.CodeAnalysis;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using VerticalSlice.Application.Shared.Infrastructure.SqlServer;
using VerticalSlice.Application.UseCases.CreateReceipt;

namespace VerticalSlice.Application.DependencyInjection
{
    [ExcludeFromCodeCoverage]
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddUseCases(this IServiceCollection services, IConfiguration configuration)
        {
            services.TryAddSingleton<IConnectionProvider, ConnectionProvider>();

            services.AddCreateReceiptUseCase(configuration);

            return services;
        }
    }
}