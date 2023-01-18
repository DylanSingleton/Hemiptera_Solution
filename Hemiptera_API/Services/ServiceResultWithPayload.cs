using Hemiptera_API.ServiceErrors;

namespace Hemiptera_API.Services
{
    public class ServiceResultWithPayload<T> : ServiceResult
    {
        public T? Payload { get; set; }
        public ServiceResultWithPayload(T payload, bool isFailure)
        {
            Payload = payload;
            IsFailure = isFailure;
        }

        public ServiceResultWithPayload(ServiceError serviceError, bool isFailure)
        {
            Errors!.Add(serviceError);
            IsFailure = isFailure;
        }
    }
}
