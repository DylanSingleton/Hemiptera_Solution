using Hemiptera_API.ServiceErrors;
using System.Net;

namespace Hemiptera_API.Services
{
    public class ServiceResultWithPayload<T> : ServiceResult
    {
        public T? Payload { get; set; }
        public ServiceResultWithPayload(T payload)
            : this(payload, false)
        {
        }

        public ServiceResultWithPayload(T payload, bool isFailure)
            : base(isFailure)
        {
            Payload = payload;
        }

        public ServiceResultWithPayload(ServiceError serviceError)
            : base(serviceError)
        {
        }
    }
}
