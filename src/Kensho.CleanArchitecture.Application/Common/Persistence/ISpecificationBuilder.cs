using Kensho.CleanArchitecture.Domain;
using System.Linq.Expressions;

namespace Kensho.CleanArchitecture.Application.Common.Persistence;

/// <summary>
/// Specificztion Builder
/// </summary>
/// <typeparam name="TEntity">The type of the entity.</typeparam>
public interface ISpecificationBuilder<TEntity> where TEntity : Entity
{
    /// <summary>
    /// Includes the specified expresion.
    /// </summary>
    /// <param name="expresion">The expresion.</param>
    /// <returns></returns>
    ISpecificationBuilder<TEntity> Include(Expression<Func<TEntity, object>> expresion);

    /// <summary>
    /// Wheres the specified expression.
    /// </summary>
    /// <param name="expression">The expression.</param>
    /// <returns></returns>
    ISpecificationBuilder<TEntity> Where(Expression<Func<TEntity, bool>> expression);

    /// <summary>
    /// Orders the by.
    /// </summary>
    /// <param name="expression">The expression.</param>
    /// <returns></returns>
    ISpecificationBuilder<TEntity> OrderBy(Expression<Func<TEntity, object>> expression);

    /// <summary>
    /// Orders the by descending.
    /// </summary>
    /// <param name="expression">The expression.</param>
    /// <returns></returns>
    ISpecificationBuilder<TEntity> OrderByDescending(Expression<Func<TEntity, object>> expression);

    /// <summary>
    /// Groups the by.
    /// </summary>
    /// <param name="expression">The expression.</param>
    /// <returns></returns>
    ISpecificationBuilder<TEntity> GroupBy(Expression<Func<TEntity, object>> expression);

    /// <summary>
    /// Pagings the specified page.
    /// </summary>
    /// <param name="page">The page.</param>
    /// <param name="pageSize">Size of the page.</param>
    /// <returns></returns>
    ISpecificationBuilder<TEntity> Paging(int page, int pageSize);

    /// <summary>
    /// Builds this instance.
    /// </summary>
    /// <returns></returns>
    ISpecification<TEntity> Build();
}
