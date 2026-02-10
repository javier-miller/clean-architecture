using Microsoft.EntityFrameworkCore;
using Kensho.CleanArchitecture.Application.Common.Persistence;
using Kensho.CleanArchitecture.Domain;
using System.Linq.Expressions;

namespace Kensho.CleanArchitecture.Infrastructure.Persistence;

/// <summary>
/// Specification
/// </summary>
/// <typeparam name="TEntity">The type of the entity.</typeparam>
/// <seealso cref="ISpecificationBuilder&lt;TEntity&gt;" />
/// <seealso cref="ISpecification&lt;TEntity&gt;" />
public class Specification<TEntity> : ISpecificationBuilder<TEntity>, ISpecification<TEntity> where TEntity : Entity
{
    private record PagingValue(int Page, int PageSize);

    private List<Expression<Func<TEntity, bool>>> _whereConditionList = new List<Expression<Func<TEntity, bool>>>();
    private Expression<Func<TEntity, object>>? _orderBy;
    private Expression<Func<TEntity, object>>? _orderByDescending;
    private List<Expression<Func<TEntity, object>>> _includeList = new List<Expression<Func<TEntity, object>>>();
    private Expression<Func<TEntity, object>>? _groupBy;
    private PagingValue? _paging;

    /// <summary>
    /// Prevents a default instance of the <see cref="Specification{TEntity}"/> class from being created.
    /// </summary>
    private Specification() { }

    /// <summary>
    /// Includes the specified expression.
    /// </summary>
    /// <param name="expression">The expression.</param>
    /// <returns></returns>
    public ISpecificationBuilder<TEntity> Include(Expression<Func<TEntity, object>> expression)
    {
        _includeList.Add(expression);
        return this;
    }

    /// <summary>
    /// Orders the by.
    /// </summary>
    /// <param name="expression">The expression.</param>
    /// <returns></returns>
    public ISpecificationBuilder<TEntity> OrderBy(Expression<Func<TEntity, object>> expression)
    {
        _orderBy = expression;
        return this;
    }

    /// <summary>
    /// Orders the by descending.
    /// </summary>
    /// <param name="expression">The expression.</param>
    /// <returns></returns>
    public ISpecificationBuilder<TEntity> OrderByDescending(Expression<Func<TEntity, object>> expression)
    {
        _orderByDescending = expression;
        return this;
    }

    /// <summary>
    /// Wheres the specified expression.
    /// </summary>
    /// <param name="expression">The expression.</param>
    /// <returns></returns>
    public ISpecificationBuilder<TEntity> Where(Expression<Func<TEntity, bool>> expression)
    {
        _whereConditionList.Add(expression);
        return this;
    }

    /// <summary>
    /// Groups the by.
    /// </summary>
    /// <param name="expression">The expression.</param>
    /// <returns></returns>
    public ISpecificationBuilder<TEntity> GroupBy(Expression<Func<TEntity, object>> expression)
    {
        _groupBy = expression;
        return this;
    }

    /// <summary>
    /// Pagings the specified page.
    /// </summary>
    /// <param name="page">The page.</param>
    /// <param name="pageSize">Size of the page.</param>
    /// <returns></returns>
    public ISpecificationBuilder<TEntity> Paging(int page, int pageSize)
    {
        _paging = new PagingValue(page, pageSize);
        return this;
    }

    /// <summary>
    /// Builds this instance.
    /// </summary>
    /// <returns></returns>
    public ISpecification<TEntity> Build() => this;

    /// <summary>
    /// Applies the specified query.
    /// </summary>
    /// <param name="query">The query.</param>
    /// <returns></returns>
    public IQueryable<TEntity> Apply(IQueryable<TEntity> query)
    {
        query = _whereConditionList.Aggregate(query, (current, exp) => current.Where(exp));
        query = _includeList.Aggregate(query, (current, include) => current.Include(include));
        query = _orderBy != default ? query.OrderBy(_orderBy) : _orderByDescending != default ? query.OrderByDescending(_orderByDescending) : query;
        if (_groupBy != null) query = query.GroupBy(_groupBy).SelectMany(x => x);
        if(_paging is not null) query = query.Skip(_paging.Page * _paging.PageSize).Take(_paging.PageSize);

        return query;
    }

    /// <summary>
    /// Creates this instance.
    /// </summary>
    /// <returns></returns>
    public static Specification<TEntity> Create() => new Specification<TEntity>();
}
