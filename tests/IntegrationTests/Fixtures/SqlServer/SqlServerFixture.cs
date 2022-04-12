using System;
using System.IO;
using System.Threading.Tasks;
using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Microsoft.SqlServer.Management.Common;
using Microsoft.SqlServer.Management.Smo;

namespace VerticalSlice.IntegratedTests.Fixtures.SqlServer
{
    internal class SqlServerFixture : IDisposable
    {
        private const string SqlServerConfiguration = "SqlServerConfiguration:ConnectionString";
        private const string SqlServerConfigurationSetup = "SqlServerConfiguration:SetupConnectionString";
        private readonly IConfiguration _configuration;
        private readonly string _connectionString;
        private readonly string _setupConnectionString;

        public SqlServerFixture(IConfiguration configuration)
        {
            _configuration = configuration;
            _connectionString = _configuration.GetSection(SqlServerConfiguration).Value;
            _setupConnectionString = _configuration.GetSection(SqlServerConfigurationSetup).Value;
        }

        public void CreateDatabase()
        {
            using var sqlConnection = new SqlConnection(_setupConnectionString);

            var server = new Server(new ServerConnection(sqlConnection: sqlConnection));

            string createScript = File.ReadAllText(string.Format(@"{0}Fixtures/SqlServer/SQL_TEMPLATE.sql", AppContext.BaseDirectory));

            server.ConnectionContext.ExecuteNonQuery(createScript);
        }

        public void Dispose()
        {
            DropDatabase();
        }

        public async Task<T> GetFirstOrDefaultAsync<T>(string sqlStatement, object filter)
        {
            using var sqlConnection = new SqlConnection(_connectionString);

            return await sqlConnection.QueryFirstOrDefaultAsync<T>(sqlStatement, filter);
        }

        private void DropDatabase()
        {
            using var sqlConnection = new SqlConnection(_setupConnectionString);

            var server = new Server(new ServerConnection(sqlConnection: sqlConnection));

            server.ConnectionContext.ExecuteNonQuery("ALTER DATABASE [DB_PAYMENTS_TEMPLATE] SET SINGLE_USER WITH ROLLBACK IMMEDIATE");
            server.ConnectionContext.ExecuteNonQuery("DROP DATABASE [DB_PAYMENTS_TEMPLATE]");
        }
    }
}