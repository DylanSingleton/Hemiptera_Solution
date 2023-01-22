using Hemiptera_API.ServiceErrors;
using System.Net;

namespace Hemiptera_API.Services
{
    public class ServiceResult
    {
        public bool IsSuccessful { get; }
        public ServiceError? Error { get; }

        public ServiceResult()
            : this(false)
        {

        }
        public ServiceResult(bool isSuccessful)
        {
            IsSuccessful = isSuccessful;
        }

        public ServiceResult(ServiceError serviceError)
            : this(false) 
        {
            Error = serviceError;
        }
    }
}
