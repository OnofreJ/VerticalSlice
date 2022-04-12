using System;

namespace VerticalSlice.WebApi.Controllers.v2.CreateReceipt
{
    public sealed class CreateReceiptRequest
    {
        public int Account { get; set; }
        public decimal Amount { get; set; }
        public Guid CorrelationId { get; set; }
        public string Login { get; set; }
    }
}