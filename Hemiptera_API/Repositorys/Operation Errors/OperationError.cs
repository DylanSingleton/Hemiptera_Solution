using Microsoft.AspNetCore.Http;
using System.Net;

namespace Hemiptera_API.ServiceErrors
{
    public class OperationError
    {
        public HttpStatusCode HttpStatusCode { get; set; }
        public string ErrorMessage { get; set; }

        public OperationError(string errorMessage, HttpStatusCode httpStatusCode)
        {
            ErrorMessage = errorMessage;
            HttpStatusCode = httpStatusCode;
        }
    }
}
