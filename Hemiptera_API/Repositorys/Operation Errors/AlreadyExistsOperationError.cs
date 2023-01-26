using Hemiptera_API.ServiceErrors;
using System.Net;

namespace Hemiptera_API.Services.Service_Errors
{
    public class AlreadyExistsOperationError : OperationError
    {
        public AlreadyExistsOperationError(string typeName, string id)
            : base($"{typeName} with the ID : {id} already exists.", HttpStatusCode.UnprocessableEntity)
        {
        }
    }
}
