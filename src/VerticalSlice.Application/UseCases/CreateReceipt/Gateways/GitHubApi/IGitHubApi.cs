using System.Threading;
using System.Threading.Tasks;
using Refit;

namespace VerticalSlice.Application.UseCases.CreateReceipt.Gateways.GitHubApi
{
    public interface IGitHubApi
    {
        [Headers("User-Agent: Awesome Orange App")]
        [Get("/users/{login}")]
        Task<ApiResponse<User>> GetUserAsync(string login, CancellationToken cancellationToken);
    }

    public class User
    {
        public string Name { get; set; }
        public string Company { get; set; }
    }
}