using Hemiptera_API.ServiceErrors;
using System.Net;

namespace Hemiptera_API.Services
{
    public class ServiceResult
    {
        public bool IsFailure { get; }
        public ICollection<ServiceError> Errors { get; }
        public ServiceResult()
            : this(false)
        {

        }
        public ServiceResult(bool isFailure)
        {
            IsFailure = isFailure;
            Errors = new List<ServiceError>();
        }

        public ServiceResult(ServiceError serviceError)
            : this(true) 
        {
            Errors.Add(serviceError);
        }
    }
}
