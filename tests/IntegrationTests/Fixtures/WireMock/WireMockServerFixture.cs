using System;
using VerticalSlice.IntegratedTests.Mocks;
using WireMock.Server;
using WireMock.Settings;

namespace VerticalSlice.IntegratedTests.Fixtures.WireMock
{
    internal sealed class WireMockServerFixture : IDisposable
    {
        private readonly WireMockServer _wireMockServer;

        public WireMockServerFixture()
        {
            _wireMockServer = WireMockServer.Start(new WireMockServerSettings
            {
                Urls = new[] { "http://+:9090" },
                StartAdminInterface = true
            });

            GitHubApiMock = new GitHubApiMock(_wireMockServer);
        }

        public GitHubApiMock GitHubApiMock { get; private set; }

        public void Dispose()
        {
            _wireMockServer.Dispose();
        }
    }
}