using System;

namespace VerticalSlice.Application.UseCases.CreateReceipt
{
    public sealed class CreateReceiptInput
    {
        public CreateReceiptInput(int account, decimal amount, Guid correlationId, string? login)
        {
            Account = account;
            Amount = amount;
            CorrelationId = correlationId;
            Login = login;
        }

        public int Account { get; }
        public decimal Amount { get; }
        public Guid CorrelationId { get; }
        public string Login { get; }
    }
}