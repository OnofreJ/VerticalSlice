using System;
using System.Collections.Generic;

namespace VerticalSlice.WebApi.Controllers
{
    public class Response<T> where T : notnull
    {
        private HashSet<string> _messages;
        private HashSet<string> _errorMessages;

        public Response(T result)
        {
            AddResult(result);
        }

        public virtual IReadOnlyCollection<string> ErrorMessages => _errorMessages;

        public virtual IReadOnlyCollection<string> Messages => _messages;

        public T Result { get; private set; }

        public void AddErrorMessage(string errorMessage)
        {
            if (string.IsNullOrWhiteSpace(errorMessage))
            {
                throw new ArgumentNullException(nameof(errorMessage));
            }

            if (_errorMessages == null)
            {
                _errorMessages = new HashSet<string>();
            }

            _errorMessages.Add(errorMessage);
        }

        public void AddResult(T result) => Result = result ?? throw new ArgumentNullException(nameof(result));

        public void AddMessage(string message)
        {
            if (string.IsNullOrWhiteSpace(message))
            {
                throw new ArgumentNullException(nameof(message));
            }

            if (_messages == null)
            {
                _messages = new HashSet<string>();
            }

            _messages.Add(message);
        }
    }
}