using System;
using System.Threading;
using System.Threading.Tasks;
using VerticalSlice.Application.Shared.Infrastructure.SqlServer;

namespace VerticalSlice.Application.UseCases.CreateReceipt.DataAccess
{
    internal sealed class ReceiptData : IReceiptData
    {
        private readonly IConnectionProvider _connectionProvider;

        public ReceiptData(IConnectionProvider connectionProvider)
        {
            _connectionProvider = connectionProvider;
        }

        public async Task InsertAsync(Guid correlationId,
            int account,
            decimal amount,
            string name,
            CancellationToken cancellationToken)
        {
            using var connection = await _connectionProvider.GetConnectionAsync(cancellationToken);
            using var sqlCommand = ReceiptDataCommand.CreateInsertCommand(correlationId, account, amount, name, connection);
            await sqlCommand.ExecuteNonQueryAsync(cancellationToken);
        }
    }
}