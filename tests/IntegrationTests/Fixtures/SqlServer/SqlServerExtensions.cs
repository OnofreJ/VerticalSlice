using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace VerticalSlice.IntegratedTests.Fixtures.SqlServer
{
    internal static class SqlServerExtensions
    {
        public static IServiceCollection AddSqlServerFixture(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddTransient(provider =>
            {
                return new SqlServerFixture(configuration);
            });

            return services;
        }
    }
}