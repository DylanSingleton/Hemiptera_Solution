using Hemiptera_API.Models;
using Hemiptera_API.Results;
using Hemiptera_API.ServiceErrors;
using Hemiptera_API.Services.Interfaces;
using Hemiptera_API.Services.Service_Errors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System.Reflection;

namespace Hemiptera_API.Services;

public class GenericRepository<T> : IGenericRepository<T> where T : class
{
    internal readonly ApplicationDbContext _context;
    internal DbSet<T>? _table = null;

    public GenericRepository(ApplicationDbContext context)
    {
        _context = context;
        _table = _context.Set<T>();
    }

    public OperationResult Delete(object id)
    {
        if (_table is null)
        {
            throw new InvalidOperationException();
        }

        T entity = _table.Find(id)!;

        if (entity is null)
        {
            return new OperationResult(new NotFoundOperationError(
                typeof(T).Name,
                id.ToString()!));
        }
        _table.Remove(entity);
        return new OperationResult(true);
    }

    public Result<List<T>> GetAll()
    {
        if(_table is null)
        {
            throw new InvalidOperationException();
        }

        var getResult = _table.ToList();

        var firstItem = getResult.FirstOrDefault();
        var entityType = firstItem?.GetType().GetGenericArguments().FirstOrDefault() ?? typeof(T);

        return getResult.Any() ? new SuccessResult<List<T>>(getResult) : new NotFoundErrorResult<List<T>>(entityType);
    }

    public OperationResultWithPayload<T> GetById(object id)
    {
        if(_table is null)
        {
            throw new InvalidOperationException();
        }

        var entity = _table.Find(id);

        return entity != null
             ? new OperationResultWithPayload<T>(entity, true)
             : new OperationResultWithPayload<T>(
                 new NotFoundOperationError(typeof(T).Name, id.ToString()));
    }

    public OperationResultWithPayload<T> Insert(T obj)
    {
        if (_table is null)
        {
            throw new InvalidOperationException();
        }
        if (_table.Contains(obj))
        {
            var id = _context.Entry(obj).Property("Id").CurrentValue!.ToString()!;
            return new OperationResultWithPayload<T>(
                new AlreadyExistsOperationError(typeof(T).Name, id));
        }
        _table.Add(obj);
        return new OperationResultWithPayload<T>(obj, true);
    }

    public OperationResultWithPayload<T> Update(T obj)
    {
        if (_table is null)
        {
            throw new InvalidOperationException();
        }
        if (!_table.Contains(obj))
        {
            var id = _context.Entry(obj).Property("Id").CurrentValue!.ToString()!;
            return new OperationResultWithPayload<T>(
                new NotFoundOperationError(typeof(T).Name, id));
        }
        _table.Attach(obj);
        _context.Entry(obj).State = EntityState.Modified;
        return new OperationResultWithPayload<T>(obj, true);
    }
}
