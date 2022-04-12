using System;
using System.Threading;
using System.Threading.Tasks;

namespace VerticalSlice.Application.UseCases.CreateReceipt.DataAccess
{
    internal interface IReceiptData
    {
        Task InsertAsync(Guid correlationId, int account, decimal amount, string name, CancellationToken cancellationToken);
    }
}