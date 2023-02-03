using Hemiptera_API.Models;
using Hemiptera_API.Results;
using Hemiptera_API.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System.Reflection;
using NotFoundResult = Hemiptera_API.Results.NotFoundResult;

namespace Hemiptera_API.Services;

public class GenericRepository<T> : IGenericRepository<T> where T : class
{
    internal readonly ApplicationDbContext _context;
    private DbSet<T>? _table = null;

    public GenericRepository(ApplicationDbContext context)
    {
        _context = context;
        _table = _context.Set<T>();
    }

    public Result Delete(object id)
    {
        if (_table is null)
        {
            throw new InvalidOperationException();
        }

        T entity = _table.Find(id);

        var entityType = entity?.GetType() ?? typeof(T);

        if (entity is null)
        {
            return new NotFoundResult(entityType, id.ToString());
        }

        _table.Remove(entity);
        return new SuccessResult();
    }

    public Result<List<T>> GetAll()
    {
        if (_table is null)
        {
            throw new InvalidOperationException();
        }

        var getResult = _table.ToList();

        var firstItem = getResult.FirstOrDefault();
        var entityType = firstItem?.GetType().GetGenericArguments().FirstOrDefault() ?? typeof(T);

        return getResult.Any()
            ? new SuccessResult<List<T>>(getResult)
            : new NotFoundResult<List<T>>(entityType);
    }

    public Result<T> GetById(object id)
    {
        if (_table is null)
        {
            throw new InvalidOperationException();
        }

        var entity = _table.Find(id);
        var entityType = entity?.GetType().GetGenericArguments().FirstOrDefault() ?? typeof(T);

        return entity != null
             ? new SuccessResult<T>(entity)
             : new NotFoundResult<T>(entityType, id.ToString());
    }

    public Result<T> Create(T obj)
    {
        if (_table is null)
        {
            throw new InvalidOperationException();
        }
        if (_table.Contains(obj))
        {
            var entityId = _context.Entry(obj).Property("Id").CurrentValue!.ToString()!;
            var entityType = obj.GetType();
            return new AlreadyExistsResult<T>(entityType, entityId);
        }
        _table.Add(obj);
        return new SuccessResult<T>(obj);
    }

    public Result<T> Update(T obj)
    {
        if (_table is null)
        {
            throw new InvalidOperationException();
        }
        if (!_table.Contains(obj))
        {
            var id = _context.Entry(obj).Property("Id").CurrentValue!.ToString()!;
            var entityType = obj.GetType();
            return new NotFoundResult<T>(entityType, id);
        }
        _table.Attach(obj);
        _context.Entry(obj).State = EntityState.Modified;
        return new SuccessResult<T>(obj);
    }
}