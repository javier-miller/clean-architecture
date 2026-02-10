namespace Kensho.CleanArchitecture.Application.Common.Persistence;

/// <summary>
/// Unit Of Work Interface
/// </summary>
public interface IUnitOfWork
{
    /// <summary>
    /// Accepts the changes asynchronous.
    /// </summary>
    /// <returns></returns>
    Task AcceptChangesAsync();
}

