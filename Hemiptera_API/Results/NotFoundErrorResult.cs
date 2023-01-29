using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace Hemiptera_API.Results
{
    public class NotFoundErrorResult<T> : ErrorResult<T>
    {
        public NotFoundErrorResult(Type entityType) : base($"{entityType.Name}(s) could not be found.")
        {
        }
    }
}
