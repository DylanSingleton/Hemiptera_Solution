namespace Hemiptera_API.Services.Interfaces
{
    public interface IGenericService<T> where T : class
    {
        ServiceResultWithPayloads<T> GetAll();
        ServiceResultWithPayload<T> GetById(object id);
        ServiceResultWithPayload<T> Insert(T obj);
        ServiceResultWithPayload<T> Update(T obj);
        ServiceResult Delete(object id);
    }
}
