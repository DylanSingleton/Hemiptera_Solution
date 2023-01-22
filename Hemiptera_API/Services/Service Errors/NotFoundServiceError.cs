using Hemiptera_API.ServiceErrors;
using System.Net;

namespace Hemiptera_API.Services.Service_Errors
{
    public class NotFoundServiceError : ServiceError
    {
        public NotFoundServiceError(string typeName, string id)
            : base($"{typeName} with the ID : {id} not found.", HttpStatusCode.NotFound)
        {
        }

        public NotFoundServiceError(string typeName)
            : base($"{typeName} table contains no records", HttpStatusCode.NotFound) 
        {
        }
    }
}
