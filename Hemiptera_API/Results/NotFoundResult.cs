using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace Hemiptera_API.Results
{
    public class NotFoundResult<T> : ErrorResult<T>
    {
        public NotFoundResult(Type entityType) : base($"{entityType.Name}(s) could not be found.")
        {
        }
    }
}
