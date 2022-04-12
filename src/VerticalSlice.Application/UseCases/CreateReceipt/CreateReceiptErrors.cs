using VerticalSlice.Application.Shared.ErrorHandling;

namespace VerticalSlice.Application.UseCases.CreateReceipt
{
    internal static class CreateReceiptErrors
    {
        public static Error ReceiptNotCreated { get; } = new Error(errorCode: "CD0001",
            errorMessage: "Bank payment receipt was not created because login is not found");

        public static Error InvalidAmount { get; } = new Error(errorCode: "CD0002",
            errorMessage: "Bank payment receipt not created because the amount is invalid");

        public static Error InvalidAccount { get; } = new Error(errorCode: "CD0003",
            errorMessage: "Bank payment receipt not created because the account is invalid");
    }
}