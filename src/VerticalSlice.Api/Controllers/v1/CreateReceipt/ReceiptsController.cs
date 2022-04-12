using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using VerticalSlice.Application.UseCases.CreateReceipt;

namespace VerticalSlice.WebApi.Controllers.v1.CreateReceipt
{
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    public sealed class ReceiptsController : ControllerBase, IOutputPort
    {
        private readonly ICreateReceiptUseCase _createReceiptUseCase;
        private IActionResult _viewModel = default!;

        public ReceiptsController(ICreateReceiptUseCase createReceiptUseCase)
        {
            _createReceiptUseCase = createReceiptUseCase;
        }

        void IOutputPort.Accepted()
        {
            _viewModel = Accepted();
        }

        void IOutputPort.NotFound(CreateReceiptOutput createReceiptOutput)
        {
            var response = new Response<string>(string.Empty);

            response.AddMessage(createReceiptOutput.Error.ErrorMessage);

            _viewModel = NotFound(response);
        }

        void IOutputPort.BadRequest(CreateReceiptOutput createReceiptOutput)
        {
            var response = new Response<string>(string.Empty);

            response.AddMessage(createReceiptOutput.Error.ErrorMessage);

            _viewModel = BadRequest(response);
        }

        [HttpPost(Name = nameof(CreateReceiptAsync))]
        [ProducesResponseType(StatusCodes.Status202Accepted)]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(Response<string>))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(Response<string>))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(Response<string>))]
        public async Task<IActionResult> CreateReceiptAsync([FromBody] CreateReceiptRequest request,
            CancellationToken cancellationToken)
        {
            _createReceiptUseCase.SetOutputPort(this);

            await _createReceiptUseCase.ExecuteAsync(new CreateReceiptInput(request.Account,
                request.Amount,
                request.CorrelationId,
                request.Login), cancellationToken);

            return _viewModel;
        }
    }
}