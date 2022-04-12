using System;

namespace VerticalSlice.IntegratedTests.Fixtures.SqlServer.Entity
{
    internal class Receipt
    {
        public int Account { get; }

        public decimal Amount { get; }

        public Guid CorrelationId { get; }

        public string? Login { get; }

        public int Id { get; }

        public string? Name { get; }
    }
}