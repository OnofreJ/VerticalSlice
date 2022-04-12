using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace VerticalSlice.Application.UseCases.CreateReceipt.Gateways.GitHubApi
{
    internal sealed class GitHubApiGateway : IGitHubApiGateway
    {
        private readonly IConfiguration _configuration;
        private readonly ILogger<GitHubApiGateway> _logger;
        private readonly IGitHubApi _gitHubApi;

        public GitHubApiGateway(IGitHubApi gitHubApi,
            ILogger<GitHubApiGateway> logger,
            IConfiguration configuration)
        {
            _logger = logger;
            _configuration = configuration;
            _gitHubApi = gitHubApi;
        }

        public async Task<string> GetUserAsync(string request, CancellationToken cancellationToken)
        {
            using var response = await _gitHubApi.GetUserAsync(request, cancellationToken);

            if (!response.IsSuccessStatusCode)
            {
                _logger.LogGatewayError(nameof(GitHubApiGateway),
                     response.ReasonPhrase!,
                     response.Error!.Message);

                if (response.StatusCode == HttpStatusCode.BadRequest ||
                    response.StatusCode == HttpStatusCode.NotFound ||
                    response.StatusCode == HttpStatusCode.Forbidden ||
                    response.StatusCode == HttpStatusCode.Unauthorized)
                {
                    return string.Empty;
                }

                throw new GitHubApiGatewayException($"[{nameof(GitHubApiGateway)}] Gateway returned with error: {response.ReasonPhrase} [{response.StatusCode}]",
                    response.StatusCode,
                    response.ReasonPhrase!);
            }

            return response.Content?.Name ?? string.Empty;
        }
    }
}