using Hemiptera_API.ServiceErrors;

namespace Hemiptera_API.Services.Service_Errors
{
    public class AlreadyExistsServiceError : ServiceError
    {
        public AlreadyExistsServiceError(string typeName, string inputId)
            : base($"{typeName} with the ID : {inputId} already exists.")
        {
        }
    }
}
