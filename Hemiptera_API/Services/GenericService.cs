using Hemiptera_API.Models;
using Microsoft.EntityFrameworkCore;

namespace Hemiptera_API.Services
{
    public class GenericService<T> : IGenericService<T> where T : class
    {
        private readonly ApplicationDbContext? _context;
        private DbSet<T>? _table;

        public GenericService(ApplicationDbContext context)
        {
            _context = context;
            _table = _context.Set<T>();
        }

        public void Delete(object id)
        {
            T entity = _table.Find(id);
            _table.Remove(entity);
        }

        public IEnumerable<T> GetAll()
        {
            return _table.ToList();
        }

        public T GetById(object id)
        {
            return _table.Find(id);
        }

        public void Insert(T obj)
        {
            _table.Add(obj);
        }

        public void Save()
        {
            _context.SaveChanges();
        }

        public void Upsert(T obj)
        {
            _table.Attach(obj);
            _context.Entry(obj).State= EntityState.Modified;
        }
    }
}
