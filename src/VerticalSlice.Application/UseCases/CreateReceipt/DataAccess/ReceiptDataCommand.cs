using System;
using System.Data;
using System.Data.SqlClient;

namespace VerticalSlice.Application.UseCases.CreateReceipt.DataAccess
{
    internal static class ReceiptDataCommand
    {
        private const string InsertCommad = @"insert into [dbo].[Receipt] (CreatedAt, CorrelationId, Account, Amount, Name)
            values (@CreatedAt, @CorrelationId, @Account, @Amount, @Name)";

        public static SqlCommand CreateInsertCommand(Guid correlationId, int account, decimal amount, string name, IDbConnection connection)
        {
            var command = new SqlCommand(InsertCommad, (SqlConnection)connection);

            command.Parameters.Add(new SqlParameter("@CorrelationId", correlationId));
            command.Parameters.Add(new SqlParameter("@CreatedAt", DateTimeOffset.Now));
            command.Parameters.Add(new SqlParameter("@Account", account));
            command.Parameters.Add(new SqlParameter("@Amount", amount));
            command.Parameters.Add(new SqlParameter("@Name", name));

            return command;
        }
    }
}