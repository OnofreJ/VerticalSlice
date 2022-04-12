using System;
using System.Net;
using WireMock.RequestBuilders;
using WireMock.ResponseBuilders;
using WireMock.Server;

namespace VerticalSlice.IntegratedTests.Mocks
{
    internal sealed class GitHubApiMock : IDisposable
    {
        private readonly WireMockServer _wireMockServer;

        public GitHubApiMock(WireMockServer wireMockServer)
        {
            _wireMockServer = wireMockServer;
        }

        public void Dispose()
        {
            _wireMockServer.ResetMappings();
        }

        internal void CreateUserResponseWith200Ok(object response, string login)
        {
            var path = $"/users/{login}";

            _wireMockServer.Given(Request.Create()
                .WithPath(path)
                .UsingGet())
                .RespondWith(Response.Create()
                    .WithBodyAsJson(response)
                    .WithHeader("Content-Type", "application/json")
                    .WithStatusCode(HttpStatusCode.OK));
        }

        internal void CreateUserResponseWith400BadRequest(string login)
        {
            var path = $"/users/{login}";

            _wireMockServer.Given(Request.Create()
                .WithPath(path)
                .UsingGet())
                .RespondWith(Response.Create()
                    .WithHeader("Content-Type", "application/json")
                    .WithStatusCode(HttpStatusCode.BadRequest));
        }

        internal void CreateUserResponseWith404NotFound(string login)
        {
            var path = $"/users/{login}";

            _wireMockServer.Given(Request.Create()
                .WithPath(path)
                .UsingGet())
                .RespondWith(Response.Create()
                    .WithHeader("Content-Type", "application/json")
                    .WithStatusCode(HttpStatusCode.NotFound));
        }

        internal void CreateUserResponseWith500ServerError(string login)
        {
            var path = $"/users/{login}";

            _wireMockServer.Given(Request.Create()
                .WithPath(path)
                .UsingGet())
                .RespondWith(Response.Create()
                    .WithHeader("Content-Type", "application/json")
                    .WithStatusCode(HttpStatusCode.InternalServerError));
        }
    }
}