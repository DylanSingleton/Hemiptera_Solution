using Hemiptera_API.ServiceErrors;
using System.Net;

namespace Hemiptera_API.Services.Service_Errors
{
    public class NotFoundOperationError : OperationError
    {
        public NotFoundOperationError(string typeName, string id)
            : base($"{typeName} with the ID : {id} not found.", HttpStatusCode.NotFound)
        {
        }

        public NotFoundOperationError(string typeName)
            : base($"{typeName} table contains no records", HttpStatusCode.NotFound) 
        {
        }
    }
}
