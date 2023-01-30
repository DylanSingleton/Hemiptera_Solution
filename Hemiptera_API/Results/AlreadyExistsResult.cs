namespace Hemiptera_API.Results
{

    public class AlreadyExistsResult<T> : ErrorResult<T>
    {
        public AlreadyExistsResult(Type entityType, string entityId) : base($"{entityType.Name} with the ID : {entityId} already exists.")
        {
        }
    }
}
