using System;
using System.Diagnostics.CodeAnalysis;
using Microsoft.Extensions.Logging;

namespace VerticalSlice.WebApi.Filters
{
    internal enum ApplicationEventType
    {
        Error
    }

    [ExcludeFromCodeCoverage]
    internal static class LoggerExtensions
    {
        private const string GlobalApiErrorMessage = "[{Path}] An unexpected error has occured with - Exception: {Exception}";

        private static Action<ILogger, string, string, Exception> GlobalApiError =>
          LoggerMessage.Define<string, string>(LogLevel.Error, new EventId((int)ApplicationEventType.Error, nameof(LogGlobalApiError)), GlobalApiErrorMessage);

        public static void LogGlobalApiError(this ILogger logger, string path, Exception exception)
        {
            GlobalApiError(logger, path, exception.Message, exception);
        }
    }
}