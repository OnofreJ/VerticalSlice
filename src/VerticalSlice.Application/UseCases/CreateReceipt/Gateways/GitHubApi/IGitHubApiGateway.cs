using System.Threading;
using System.Threading.Tasks;

namespace VerticalSlice.Application.UseCases.CreateReceipt.Gateways.GitHubApi
{
    internal interface IGitHubApiGateway
    {
        Task<string> GetUserAsync(string request, CancellationToken cancellationToken);
    }
}