using System.Threading;
using System.Threading.Tasks;

namespace VerticalSlice.Application.UseCases.CreateReceipt
{
    internal sealed class CreateReceiptUseCaseValidation : ICreateReceiptUseCase
    {
        private readonly ICreateReceiptUseCase _createReceiptUseCase;
        private IOutputPort _outputPort = default!;

        public CreateReceiptUseCaseValidation(ICreateReceiptUseCase createReceiptUseCase)
        {
            _createReceiptUseCase = createReceiptUseCase;
        }

        public async Task ExecuteAsync(CreateReceiptInput input, CancellationToken cancellationToken)
        {
            if (input.Amount <= decimal.Zero)
            {
                _outputPort.BadRequest(CreateReceiptOutput.Fail(CreateReceiptErrors.InvalidAmount));

                return;
            }

            if (input.Account <= decimal.Zero)
            {
                _outputPort.BadRequest(CreateReceiptOutput.Fail(CreateReceiptErrors.InvalidAccount));

                return;
            }

            await _createReceiptUseCase.ExecuteAsync(input, cancellationToken);
        }

        public void SetOutputPort(IOutputPort outputPort)
        {
            _outputPort = outputPort;

            _createReceiptUseCase.SetOutputPort(outputPort);
        }
    }
}