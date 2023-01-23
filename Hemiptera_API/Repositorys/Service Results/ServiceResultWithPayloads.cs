using Hemiptera_API.ServiceErrors;
using System.Net;

namespace Hemiptera_API.Services
{
    public class ServiceResultWithPayloads<T> : ServiceResult
    {
        public ICollection<T>? Payloads { get; set; }
        public ServiceResultWithPayloads(ICollection<T> payloads)
            : this(payloads, false)
        {
        }

        public ServiceResultWithPayloads(ICollection<T> payloads, bool IsSuccessful)
            : base(IsSuccessful)
        {
            Payloads = payloads;
        }

        public ServiceResultWithPayloads(ServiceError serviceError)
            : base(serviceError)
        {
        }
    }
}
