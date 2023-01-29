using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace Hemiptera_API.Results
{
    public class NotFoundResult<T> : ErrorResult<T>
    {
        public NotFoundResult(Type entityType) : base($"{entityType.Name}(s) could not be found.")
        {
        }

        public NotFoundResult(Type entityType, string entityId) : base($"{entityType.Name} could not be found with the ID : {entityId}.")
        {
        }

    }

    public class NotFoundResult : ErrorResult
    {
        public NotFoundResult(Type entityType) : base($"{entityType.Name}(s) could not be found.")
        {
        }

        public NotFoundResult(Type entityType, string entityId) : base($"{entityType.Name} could not be found with the ID : {entityId}.")
        {
        }

    }
}
