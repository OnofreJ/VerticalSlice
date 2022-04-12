using System;
using System.Diagnostics.CodeAnalysis;
using Microsoft.Extensions.Logging;

namespace VerticalSlice.Application.UseCases.CreateReceipt.Gateways
{
    internal enum GatewayEventType
    {
        Error
    }

    [ExcludeFromCodeCoverage]
    internal static class LoggerExtensions
    {
        private const string GatewayErrorMessage = "[{Type}] Gateway returned with the following error: {ReasonPhrase} / {Message}";

        private static Action<ILogger, string, string, string, Exception> GatewayError =>
            LoggerMessage.Define<string, string, string>(LogLevel.Error, new EventId((int)GatewayEventType.Error, nameof(LogGatewayError)), GatewayErrorMessage);

        public static void LogGatewayError(this ILogger logger, string type, string reasonPhrase, string message)
        {
            GatewayError(logger, type, reasonPhrase, message, default);
        }
    }
}