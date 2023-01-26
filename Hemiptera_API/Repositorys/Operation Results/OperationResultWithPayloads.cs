using Hemiptera_API.ServiceErrors;
using System.Net;

namespace Hemiptera_API.Services
{
    public class OperationResultWithPayloads<T> : OperationResult
    {
        public ICollection<T>? Payloads { get; set; }
        public OperationResultWithPayloads(ICollection<T> payloads)
            : this(payloads, false)
        {
        }

        public OperationResultWithPayloads(ICollection<T> payloads, bool IsSuccessful)
            : base(IsSuccessful)
        {
            Payloads = payloads;
        }

        public OperationResultWithPayloads(OperationError serviceError)
            : base(serviceError)
        {
        }
    }
}
