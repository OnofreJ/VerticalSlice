using VerticalSlice.Application.Shared.ErrorHandling;

namespace VerticalSlice.Application.UseCases.CreateReceipt
{
    public sealed class CreateReceiptOutput
    {
        private CreateReceiptOutput(Error error = default)
        {
            Error = error;
            HasFailed = !string.IsNullOrWhiteSpace(Error.ErrorMessage);
        }

        public Error Error { get; private set; }

        public bool HasFailed { get; private set; }

        public static CreateReceiptOutput Fail(Error error)
        {
            return new CreateReceiptOutput(error);
        }

        public static CreateReceiptOutput Success()
        {
            return new CreateReceiptOutput();
        }
    }
}