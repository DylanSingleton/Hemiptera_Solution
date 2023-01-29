using Hemiptera_API.Results;

namespace Hemiptera_API.Services.Interfaces
{
    public interface IGenericRepository<T> where T : class
    {
        Result<List<T>> GetAll();
        OperationResultWithPayload<T> GetById(object id);
        OperationResultWithPayload<T> Insert(T obj);
        OperationResultWithPayload<T> Update(T obj);
        Result Delete(object id);
    }
}
