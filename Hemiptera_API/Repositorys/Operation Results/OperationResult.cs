using Hemiptera_API.ServiceErrors;
using System.Net;

namespace Hemiptera_API.Services
{
    public class OperationResult
    {
        public bool IsSuccessful { get; }
        public OperationError? Error { get; }

        public OperationResult()
            : this(false)
        {

        }
        public OperationResult(bool isSuccessful)
        {
            IsSuccessful = isSuccessful;
        }

        public OperationResult(OperationError serviceError)
            : this(false) 
        {
            Error = serviceError;
        }
    }
}
