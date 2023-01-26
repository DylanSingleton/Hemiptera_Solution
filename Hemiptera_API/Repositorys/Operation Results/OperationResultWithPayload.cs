using Hemiptera_API.ServiceErrors;
using System.Net;

namespace Hemiptera_API.Services
{
    public class OperationResultWithPayload<T> : OperationResult
    {
        public T? Payload { get; set; }
        public OperationResultWithPayload(T payload)
            : this(payload, false)
        {
        }

        public OperationResultWithPayload(T payload, bool IsSuccessful)
            : base(IsSuccessful)
        {
            Payload = payload;
        }

        public OperationResultWithPayload(OperationError serviceError)
            : base(serviceError)
        {
        }
    }
}
