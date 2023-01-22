using Microsoft.AspNetCore.Http;
using System.Net;

namespace Hemiptera_API.ServiceErrors
{
    public class ServiceError
    {
        public HttpStatusCode HttpStatusCode { get; set; }
        public string ErrorMessage { get; set; }

        public ServiceError(string errorMessage, HttpStatusCode httpStatusCode)
        {
            ErrorMessage = errorMessage;
            HttpStatusCode = httpStatusCode;
        }
    }
}
