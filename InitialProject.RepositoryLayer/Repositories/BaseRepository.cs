using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using System.Linq.Expressions;
using InitialProject.Core;
using InitialProject.RepositoryLayer.Interfaces;

namespace InitialProject.RepositoryLayer.Repositories;

public class BaseRepository<T> : IBaseRepository<T> where T : class
{
    protected ApplicationDbContext Context;

    public BaseRepository(ApplicationDbContext context)
    {
        Context = context;
    }

    public IEnumerable<T> GetAll(Func<IQueryable<T>, IIncludableQueryable<T, object>> include = null, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null)
    {
        IQueryable<T> query = Context.Set<T>();
        if (include != null)
            query = include(query);
        if (orderBy != null)
            query = orderBy(query);

        return query.ToList();
    }

    public async Task<IEnumerable<T>> GetAllAsync(Func<IQueryable<T>, IIncludableQueryable<T, object>> include = null, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null)
    {
        IQueryable<T> query = Context.Set<T>();
        if (include != null)
            query = include(query);
        if (orderBy != null)
            query = orderBy(query);
        return await query.ToListAsync();
    }

    public T GetById(int id)
    {
        return Context.Set<T>().Find(id);
    }

    public async Task<T> GetByIdAsync(int id)
    {
        return await Context.Set<T>().FindAsync(id);
    }

    //-------------------------------------------------------------------------------------------

    public T Find(Expression<Func<T, bool>> criteria, Func<IQueryable<T>, IIncludableQueryable<T, object>> include = null, bool isNoTracking = false)
    {
        IQueryable<T> query = Context.Set<T>();
        if (include != null)
            query = include(query);
        if (isNoTracking)
            query = query.AsNoTracking();

        return query.FirstOrDefault(criteria);
    }

    public async Task<T> FindAsync(Expression<Func<T, bool>> criteria, Func<IQueryable<T>, IIncludableQueryable<T, object>> include = null, bool isNoTracking = false)
    {
        IQueryable<T> query = Context.Set<T>();

        if (include != null)
            query = include(query);
        if (isNoTracking)
            query = query.AsNoTracking();

        return await query.FirstOrDefaultAsync(criteria);
    }

    public IEnumerable<T> FindAll(
        Expression<Func<T, bool>> criteria,
        int? take = null, int? skip = null,
        Func<IQueryable<T>, IIncludableQueryable<T, object>> include = null,
        Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null, bool isNoTracking = false
    )
    {
        IQueryable<T> query = Context.Set<T>();

        if (include != null)
            query = include(query);

        query = query.Where(criteria);

        if (skip != null)
            query = query.Skip(skip.Value);

        if (take != null)
            query = query.Take(take.Value);

        if (orderBy != null)
            query = orderBy(query);

        if (isNoTracking)
            query = query.AsNoTracking();

        return query.ToList();
    }

    public async Task<IEnumerable<T>> FindAllAsync(
        Expression<Func<T, bool>> criteria,
        int? take = null, int? skip = null,
        Func<IQueryable<T>, IIncludableQueryable<T, object>> include = null,
        Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null, bool isNoTracking = false
    )
    {
        IQueryable<T> query = Context.Set<T>();

        if (include != null)
            query = include(query);

        query = query.Where(criteria);

        if (skip != null)
            query = query.Skip(skip.Value);

        if (take != null)
            query = query.Take(take.Value);

        if (orderBy != null)
            query = orderBy(query);

        if (isNoTracking)
            query = query.AsNoTracking();

        return await query.ToListAsync();
    }

    public IQueryable<T> FindByQuery(
        Expression<Func<T, bool>> criteria,
        int? take = null, int? skip = null,
        Func<IQueryable<T>, IIncludableQueryable<T, object>> include = null,
        Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null, bool isNoTracking = false
    )
    {
        IQueryable<T> query = Context.Set<T>();

        if (include != null)
            query = include(query);

        query = query.Where(criteria);

        if (skip.HasValue)
            query = query.Skip(skip.Value);

        if (take != null)
            query = query.Take(take.Value);

        if (orderBy != null)
            query = orderBy(query);

        if (isNoTracking)
            query = query.AsNoTracking();

        return query;
    }

    //-------------------------------------------------------------------
    public T Add(T entity)
    {
        Context.Set<T>().Add(entity);
        return entity;
    }

    public async Task<T> AddAsync(T entity)
    {
        await Context.Set<T>().AddAsync(entity);
        return entity;
    }

    public IEnumerable<T> AddRange(IEnumerable<T> entities)
    {
        Context.Set<T>().AddRange(entities);
        return entities;
    }

    public async Task<IEnumerable<T>> AddRangeAsync(IEnumerable<T> entities)
    {
        await Context.Set<T>().AddRangeAsync(entities);
        return entities;
    }

    //-------------------------------------------------------------------
    public bool IsExist(Expression<Func<T, bool>> criteria)
    {
        return Context.Set<T>().Any(criteria);
    }

    //-------------------------------------------------------------------
    public T Update(T entity)
    {
        Context.Update(entity);
        return entity;
    }

    public void Delete(T entity)
    {
        Context.Set<T>().Remove(entity);
    }

    public void DeleteRange(IEnumerable<T> entities)
    {
        Context.Set<T>().RemoveRange(entities);
    }

    public void Attach(T entity)
    {
        Context.Set<T>().Attach(entity);
    }

    public void DeAttach(T entity)
    {
        Context.Entry(entity).State = EntityState.Detached;
    }

    public void AttachRange(IEnumerable<T> entities)
    {
        Context.Set<T>().AttachRange(entities);
    }

    //-------------------------------------------------------------------
    public int Count()
    {
        return Context.Set<T>().Count();
    }

    public int Count(Expression<Func<T, bool>> criteria)
    {
        return Context.Set<T>().Count(criteria);
    }

    public async Task<int> CountAsync()
    {
        return await Context.Set<T>().CountAsync();
    }

    public async Task<int> CountAsync(Expression<Func<T, bool>> criteria)
    {
        return await Context.Set<T>().CountAsync(criteria);
    }
}