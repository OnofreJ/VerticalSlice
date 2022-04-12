using System.Net;
using System.Net.Http.Json;
using System.Threading.Tasks;
using AutoFixture;
using FluentAssertions;
using VerticalSlice.Application.UseCases.CreateReceipt.Gateways.GitHubApi;
using VerticalSlice.IntegratedTest.Fixtures;
using VerticalSlice.IntegratedTests.Fixtures.SqlServer;
using VerticalSlice.IntegratedTests.Fixtures.SqlServer.Entity;
using VerticalSlice.IntegratedTests.Fixtures.WireMock;
using VerticalSlice.WebApi.Controllers.v1.CreateReceipt;
using Xunit;

namespace VerticalSlice.IntegratedTests.Tests.CreateReceipt
{
    public class CreateReceiptTests : BaseIntegratedTest, IClassFixture<MainFixture>
    {
        private const string Endpoint = "api/v1/receipts";

        private const string Query = "SELECT [Id],[Name],[CreatedAt],[CorrelationId],[Account],[Amount] " +
            "FROM [dbo].[Receipt] where [CorrelationId] = @CorrelationId";

        private readonly MainFixture _mainFixture;
        private readonly Fixture _fixture;
        private readonly WireMockServerFixture _wireMockServerFixture;
        private readonly SqlServerFixture _sqlServerFixture;

        public CreateReceiptTests(MainFixture mainFixture)
        {
            _fixture = new Fixture();
            _mainFixture = mainFixture;
            _sqlServerFixture = _mainFixture.SqlServerFixture;
            _wireMockServerFixture = _mainFixture.MockServerFixture;
        }

        [Fact(DisplayName = "Receipt creation is requested, input is valid and the receipt is inserted into the database")]
        public async Task ReceiptCreationRequested_GatewayRespondsCorrectly_ReceiptInsertedDatabase()
        {
            //Arrange
            var request = _fixture.Create<CreateReceiptRequest>();
            var userResponse = _fixture.Create<User>();

            _wireMockServerFixture.GitHubApiMock.CreateUserResponseWith200Ok(userResponse, request.Login);

            //Act
            var response = await _mainFixture.HttpClient.PostAsJsonAsync(Endpoint, request, GetCancellationToken);

            //Assert
            response.IsSuccessStatusCode.Should().BeTrue();
            response.StatusCode.Should().Be(HttpStatusCode.Accepted);

            var receipt = await _sqlServerFixture.GetFirstOrDefaultAsync<Receipt>(Query, new { request.CorrelationId });

            receipt.CorrelationId.Should().Be(request.CorrelationId);
            receipt.Account.Should().Be(request.Account);
            receipt.Amount.Should().Be(request.Amount);
            receipt.Name.Should().Be(userResponse.Name);
        }

        [Theory(DisplayName = "Receipt creation is requested, input is not valid then API responds with bad request")]
        [InlineData(0, 100)]
        [InlineData(100, 0)]
        public async Task ReceiptCreationRequested_InputIsNotValid_ApiRespondsWithBadRequest(int account, decimal amount)
        {
            //Arrange
            var request = _fixture.Build<CreateReceiptRequest>()
                .With(property => property.Amount, amount)
                .With(property => property.Account, account)
                .Create();

            //Act
            var response = await _mainFixture.HttpClient.PostAsJsonAsync(Endpoint, request, GetCancellationToken);

            //Assert
            response.IsSuccessStatusCode.Should().BeFalse();
            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }

        [Fact(DisplayName = "Receipt creation is requested, input is valid and gateway responds with not found then API responds with not found")]
        public async Task ReceiptCreationRequested_InputIsValid_GatewayRespondsWithNotFound()
        {
            //Arrange
            var request = _fixture.Create<CreateReceiptRequest>();

            _wireMockServerFixture.GitHubApiMock.CreateUserResponseWith404NotFound(_fixture.Create<string>());

            //Act
            var response = await _mainFixture.HttpClient.PostAsJsonAsync(Endpoint, request, GetCancellationToken);

            //Assert
            response.IsSuccessStatusCode.Should().BeFalse();
            response.StatusCode.Should().Be(HttpStatusCode.NotFound);
        }

        [Fact(DisplayName = "Receipt creation is requested, input is valid then gateway responds with error")]
        public async Task ReceiptCreationRequested_InputIsValid_GatewayRespondsWithError()
        {
            //Arrange
            var request = _fixture.Create<CreateReceiptRequest>();

            _wireMockServerFixture.GitHubApiMock.CreateUserResponseWith500ServerError(request.Login);

            //Act
            var response = await _mainFixture.HttpClient.PostAsJsonAsync(Endpoint, request, GetCancellationToken);

            //Assert
            response.IsSuccessStatusCode.Should().BeFalse();
            response.StatusCode.Should().Be(HttpStatusCode.InternalServerError);
        }
    }
}