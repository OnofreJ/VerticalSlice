using System.Data;
using System.Threading;
using System.Threading.Tasks;

namespace VerticalSlice.Application.Shared.Infrastructure.SqlServer
{
    public interface IConnectionProvider
    {
        Task<IDbConnection> GetConnectionAsync(CancellationToken cancellationToken);
    }
}