namespace VerticalSlice.Application.UseCases.CreateReceipt
{
    public interface IOutputPort
    {
        void Accepted();

        void NotFound(CreateReceiptOutput createReceiptOutput);

        void BadRequest(CreateReceiptOutput createReceiptOutput);
    }
}