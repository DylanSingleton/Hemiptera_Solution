namespace Hemiptera_API.Services.Interfaces
{
    public interface IGenericService<T> where T : class
    {
        ServiceResultWithPayloads<T> GetAll();
        ServiceResultWithPayload<T> GetById(object id);
        ServiceResult Insert(T obj);
        ServiceResult Upsert(T obj);
        ServiceResult Delete(object id);
    }
}
