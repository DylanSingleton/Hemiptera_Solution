using Hemiptera_API.ServiceErrors;
using System.Net;

namespace Hemiptera_API.Services
{
    public class ServiceResultWithPayloads<T> : ServiceResult
    {
        public List<T>? Payload { get; set; }
        public ServiceResultWithPayloads()
        {
            Errors = new List<ServiceError>();
            Payload = new List<T>();
        }

        public ServiceResultWithPayloads(List<T> payload, bool isFailure)
        {
            Payload = payload;
            IsFailure = isFailure;
        }

        public ServiceResultWithPayloads(ServiceError serviceError, bool isFailure)
        {
            Errors!.Add(serviceError);
            IsFailure = isFailure;
        }
    }
}
