using System.Threading;
using System.Threading.Tasks;
using VerticalSlice.Application.UseCases.CreateReceipt.DataAccess;
using VerticalSlice.Application.UseCases.CreateReceipt.Gateways.GitHubApi;

namespace VerticalSlice.Application.UseCases.CreateReceipt
{
    internal sealed class CreateReceiptUseCase : ICreateReceiptUseCase
    {
        private readonly IGitHubApiGateway _gitHubApiGateway;
        private readonly IReceiptData _receiptData;
        private IOutputPort _outputPort = default!;

        public CreateReceiptUseCase(IReceiptData receiptData, IGitHubApiGateway gitHubApiGateway)
        {
            _gitHubApiGateway = gitHubApiGateway;
            _receiptData = receiptData;
        }

        public async Task ExecuteAsync(CreateReceiptInput input, CancellationToken cancellationToken)
        {
            var name = await _gitHubApiGateway.GetUserAsync(input.Login, cancellationToken);

            if (string.IsNullOrWhiteSpace(name))
            {
                _outputPort.NotFound(CreateReceiptOutput.Fail(CreateReceiptErrors.ReceiptNotCreated));

                return;
            }

            await _receiptData.InsertAsync(input.CorrelationId,
                input.Account,
                input.Amount,
                name,
                cancellationToken);

            _outputPort.Accepted();
        }

        public void SetOutputPort(IOutputPort outputPort)
        {
            _outputPort = outputPort;
        }
    }
}