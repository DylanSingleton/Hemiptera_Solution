namespace Hemiptera_API.Services
{
    public interface IGenericService<T> where T : class
    {
        IEnumerable<T> GetAll();
        T GetById(object id);
        void Insert(T obj);
        void Upsert(T obj);
        void Delete(object id);
        void Save();
    }
}
