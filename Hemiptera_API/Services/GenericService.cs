using Hemiptera_API.Models;
using Hemiptera_API.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace Hemiptera_API.Services
{
    public class GenericService<T> : IGenericService<T> where T : class
    {
        internal ApplicationDbContext _context;
        internal DbSet<T>? _table = null;

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

        public void Upsert(T obj)
        {
            _table.Attach(obj);
            _context.Entry(obj).State= EntityState.Modified;
        }
    }
}
