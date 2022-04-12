using System;
using System.Net.Http;
using Microsoft.Extensions.Configuration;
using VerticalSlice.IntegratedTests.Factories;
using VerticalSlice.IntegratedTests.Fixtures.SqlServer;
using VerticalSlice.IntegratedTests.Fixtures.WireMock;

namespace VerticalSlice.IntegratedTest.Fixtures
{
    public class MainFixture : IDisposable
    {
        private readonly HttpClient _httpClient;
        private readonly SqlServerFixture _sqlServerFixture;
        private readonly WireMockServerFixture _mockServerFixture;

        public MainFixture()
        {
            var configuration = new ConfigurationBuilder()
              .SetBasePath(AppContext.BaseDirectory)
              .AddJsonFile("appsettings.json", optional: false)
              .Build();

            _httpClient = new CustomWebApplicationFactory().CreateClient();
            _mockServerFixture = new WireMockServerFixture();
            _sqlServerFixture = new SqlServerFixture(configuration);
            _sqlServerFixture.CreateDatabase();
        }

        internal WireMockServerFixture MockServerFixture => _mockServerFixture;

        public HttpClient HttpClient => _httpClient;

        internal SqlServerFixture SqlServerFixture => _sqlServerFixture;

        public void Dispose()
        {
            _httpClient.Dispose();
            _mockServerFixture.Dispose();
            _sqlServerFixture?.Dispose();

            GC.SuppressFinalize(this);
        }
    }
}