using System;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics.CodeAnalysis;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;

namespace VerticalSlice.Application.Shared.Infrastructure.SqlServer
{
    [ExcludeFromCodeCoverage]
    internal sealed class ConnectionProvider : IConnectionProvider
    {
        private const string SqlServerConfiguration = "SqlServerConfiguration:ConnectionString";
        private readonly string _connectionString;

        public ConnectionProvider(IConfiguration configuration)
        {
            _connectionString = configuration.GetSection(SqlServerConfiguration).Value;

            if (string.IsNullOrWhiteSpace(_connectionString))
            {
                throw new ArgumentException(SqlServerConfiguration);
            }
        }

        public async Task<IDbConnection> GetConnectionAsync(CancellationToken cancellationToken)
        {
            var sqlConnection = new SqlConnection(_connectionString);
            await sqlConnection.OpenAsync(cancellationToken);
            return sqlConnection;
        }
    }
}