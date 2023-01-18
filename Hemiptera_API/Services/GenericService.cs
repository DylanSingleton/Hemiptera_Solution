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
            ServiceResult result = new ServiceResult();

            if(_table is null)
            {
                throw new InvalidOperationException();
            }

            T entity = _table.Find(id)!;

            if(entity is null)
            {
                result.IsFailure = true;
                result.Errors.Add(new ServiceError($"Entity with the ID : {id} not found."));
            }
                _table.Remove(entity!);
                return result;
        }

        public ServiceResultWithPayloads<T> GetAll()
        {
            if(_table is null)
            {
                throw new InvalidOperationException();
            }

            var getResult = _table.ToList();

            if(getResult.Any())
            {
                return new ServiceResultWithPayloads<T>(
                    getResult,
                    false);
            }

            return new ServiceResultWithPayloads<T>(
                new ServiceError($"{typeof(T).Name} contains no records."),
                true);
        }

        public ServiceResultWithPayload<T> GetById(object id)
        {
            if(_table is null)
            {
                throw new InvalidOperationException();
            }

            var entity = _table.Find(id);

            if(entity is not null)
            {
                return new ServiceResultWithPayload<T>(entity, false);
            }

            return new ServiceResultWithPayload<T>(
                new ServiceError($"{typeof(T).Name} with the ID : {id} not found."),
                true);
        }

        public ServiceResult Insert(T obj)
        {
            ServiceResult result = new();

            if (_table is null)
            {
                result.IsFailure = true;
                throw new InvalidOperationException();
            }

            _table.Add(obj);
            return result;
        }

        public ServiceResult Upsert(T obj)
        {
            ServiceResult result = new();

            _table.Attach(obj);
            _context.Entry(obj).State = EntityState.Modified;

            return result;
        }
    }
}
