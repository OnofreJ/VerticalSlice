using System;
using System.Diagnostics.CodeAnalysis;
using System.Net;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;
using VerticalSlice.WebApi.Controllers;

namespace VerticalSlice.WebApi.Filters
{
    [ExcludeFromCodeCoverage]
    internal sealed class ExceptionFilter : IExceptionFilter
    {
        private const string DefaultErrorMessage = "An unexpected error occurred - check logs for details.";
        private readonly ILogger _logger;

        public ExceptionFilter(ILogger<ExceptionFilter> logger)
        {
            _logger = logger;
        }

        public void OnException(ExceptionContext context)
        {
            var response = new Response<string>(string.Empty);

            response.AddErrorMessage(DefaultErrorMessage);

            LogGetErrorMessages(context, context.Exception);

            context.Result = new ObjectResult(response)
            {
                StatusCode = (int)HttpStatusCode.InternalServerError
            };

            context.ExceptionHandled = true;
        }

        private void LogGetErrorMessages(ExceptionContext context, Exception exception)
        {
            _logger.LogGlobalApiError(context.HttpContext.Request.Path, exception);

            if (exception?.InnerException != null)
            {
                LogGetErrorMessages(context, exception.InnerException);
            }
        }
    }
}