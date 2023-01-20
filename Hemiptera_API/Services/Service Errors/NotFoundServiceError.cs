using Hemiptera_API.ServiceErrors;

namespace Hemiptera_API.Services.Service_Errors
{
    public class NotFoundServiceError : ServiceError
    {
        public NotFoundServiceError(string typeName, string inputId)
            : base($"{typeName} with the ID : {inputId} not found.")
        {
        }

        public NotFoundServiceError(string typeName)
            : base($"{typeName} contains no records") 
        {
        }

    }
}
