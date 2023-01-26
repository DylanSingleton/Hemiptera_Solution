namespace Hemiptera_API.Services.Interfaces
{
    public interface IGenericRepository<T> where T : class
    {
        OperationResultWithPayloads<T> GetAll();
        OperationResultWithPayload<T> GetById(object id);
        OperationResultWithPayload<T> Insert(T obj);
        OperationResultWithPayload<T> Update(T obj);
        OperationResult Delete(object id);
    }
}
