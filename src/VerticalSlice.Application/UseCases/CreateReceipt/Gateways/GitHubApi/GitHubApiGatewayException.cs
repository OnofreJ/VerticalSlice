using System;
using System.Diagnostics.CodeAnalysis;
using System.Net;

namespace VerticalSlice.Application.UseCases.CreateReceipt.Gateways.GitHubApi
{
    [ExcludeFromCodeCoverage]
    [Serializable]
    public class GitHubApiGatewayException : Exception
    {
        public GitHubApiGatewayException(HttpStatusCode statusCode, string reasonPhrase)
        {
            StatusCode = statusCode;
            ReasonPhrase = reasonPhrase;
        }

        public GitHubApiGatewayException(string message, HttpStatusCode statusCode, string reasonPhrase) : base(message)
        {
            StatusCode = statusCode;
            ReasonPhrase = reasonPhrase;
        }

        public GitHubApiGatewayException(string message, HttpStatusCode statusCode, string reasonPhrase, Exception innerException) : base(message, innerException)
        {
            StatusCode = statusCode;
            ReasonPhrase = reasonPhrase;
        }

        public string ReasonPhrase { get; }

        public HttpStatusCode StatusCode { get; }
    }
}