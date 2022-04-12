using System.Diagnostics.CodeAnalysis;
using VerticalSlice.Application.Shared.ErrorHandling;

namespace VerticalSlice.Application.UseCases.CreateReceipt.Gateways
{
    [ExcludeFromCodeCoverage]
    internal static class GatewaysErrors
    {
        public static Error FutureEntriesPublisherGatewayError { get; } = new Error(errorCode: "GTW002",
            errorMessage: "Future entry was not created - error at future entries publisher gateway");

        public static Error ReceiptsPublisherGatewayError { get; } = new Error(errorCode: "GTW001",
            errorMessage: "Receipt was not created - error at receipts publisher gateway");
    }
}