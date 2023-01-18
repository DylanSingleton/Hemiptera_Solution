using Hemiptera_API.Models;
using Hemiptera_API.ServiceErrors;
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

        public ServiceResult Delete(object id)
        {
            if (_table is null)
            {
                throw new InvalidOperationException();
            }

            T entity = _table.Find(id)!;

            if (entity is null)
            {
                return new ServiceResult(new ServiceError($"Entity to delete with the ID : {id} not found."));
            }
            _table.Remove(entity!);
            return new ServiceResult(false);
        }

        public ServiceResultWithPayloads<T> GetAll()
        {
            if(_table is null)
            {
                throw new InvalidOperationException();
            }

            var getResult = _table.ToList();

            return getResult.Any()
              ? new ServiceResultWithPayloads<T>(getResult, false)
              : new ServiceResultWithPayloads<T>(new ServiceError($"{typeof(T).Name} contains no records."));
        }

        public ServiceResultWithPayload<T> GetById(object id)
        {
            if(_table is null)
            {
                throw new InvalidOperationException();
            }

            var entity = _table.Find(id);

            return entity != null
                 ? new ServiceResultWithPayload<T>(entity, false)
                 : new ServiceResultWithPayload<T>(new ServiceError($"{typeof(T).Name} with the ID : {id} not found."));
        }

        public ServiceResultWithPayload<T> Insert(T obj)
        {
            if (_table is null)
            {
                throw new InvalidOperationException();
            }
            if (_table.Contains(obj))
            {
                return new ServiceResultWithPayload<T>(new ServiceError($"{typeof(T).Name} already exists."));
            }
            _table.Add(obj);
            return new ServiceResultWithPayload<T>(obj);
        }

        public ServiceResultWithPayload<T> Update(T obj)
        {
            if (_table is null)
            {
                throw new InvalidOperationException();
            }
            if (!_table.Contains(obj))
            {
                return new ServiceResultWithPayload<T>(new ServiceError($"{typeof(T).Name} not found."));
            }
            _table.Attach(obj);
            _context.Entry(obj).State = EntityState.Modified;
            return new ServiceResultWithPayload<T>(obj);
        }
    }
}
