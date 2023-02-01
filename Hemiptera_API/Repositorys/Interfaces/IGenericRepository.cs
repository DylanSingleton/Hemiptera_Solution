using Hemiptera_API.Results;

namespace Hemiptera_API.Services.Interfaces;

public interface IGenericRepository<T> where T : class
{
    Result<List<T>> GetAll();

    Result<T> GetById(object id);

    Result<T> Create(T obj);

    Result<T> Update(T obj);

    Result Delete(object id);
}