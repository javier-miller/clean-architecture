using Microsoft.EntityFrameworkCore;
using Kensho.CleanArchitecture.Application.Common.Persistence;
using Kensho.CleanArchitecture.Domain;
using Kensho.CleanArchitecture.Domain.Events;

namespace Kensho.CleanArchitecture.Infrastructure.Persistence;

/// <summary>
/// Application Db Context
/// </summary>
/// <typeparam name="TDbContext">The type of the database context.</typeparam>
/// <seealso cref="DbContext" />
/// <seealso cref="IUnitOfWork" />
public abstract class ApplicationDbContext<TDbContext> : DbContext, IUnitOfWork where TDbContext : DbContext
{
    protected readonly bool _useTracking = true;
    protected readonly IDomainEventsManager _eventsManager;

    /// <summary>
    /// Initializes a new instance of the <see cref="ApplicationDbContext{TDbContext}"/> class.
    /// </summary>
    /// <param name="options">The options.</param>
    /// <param name="eventsManager">The events manager.</param>
    /// <param name="useTracking">if set to <c>true</c> [use tracking].</param>
    /// <exception cref="ArgumentNullException"></exception>
    public ApplicationDbContext(DbContextOptions options, IDomainEventsManager eventsManager, bool useTracking = true) : base(options)
    {
        ArgumentNullException.ThrowIfNull(eventsManager);

        _eventsManager = eventsManager;
        _useTracking = useTracking;
    }

    /// <summary>
    /// Override this method to configure the database (and other options) to be used for this context.
    /// This method is called for each instance of the context that is created.
    /// The base implementation does nothing.
    /// </summary>
    /// <param name="optionsBuilder">A builder used to create or modify options for this context. Databases (and other extensions)
    /// typically define extension methods on this object that allow you to configure the context.</param>
    /// <remarks>
    /// <para>
    /// In situations where an instance of <see cref="T:Microsoft.EntityFrameworkCore.DbContextOptions" /> may or may not have been passed
    /// to the constructor, you can use <see cref="P:Microsoft.EntityFrameworkCore.DbContextOptionsBuilder.IsConfigured" /> to determine if
    /// the options have already been set, and skip some or all of the logic in
    /// <see cref="M:Microsoft.EntityFrameworkCore.DbContext.OnConfiguring(Microsoft.EntityFrameworkCore.DbContextOptionsBuilder)" />.
    /// </para>
    /// <para>
    /// See <see href="https://aka.ms/efcore-docs-dbcontext">DbContext lifetime, configuration, and initialization</see>
    /// for more information and examples.
    /// </para>
    /// </remarks>
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!_useTracking) optionsBuilder.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
    }

    /// <summary>
    /// Accepts the changes asynchronous.
    /// </summary>
    public async Task AcceptChangesAsync()
    {
        var domainEvents = ChangeTracker.Entries<IEntity>()
            .Select(entry => entry.Entity.PopDomainEvents())
            .SelectMany(e => e).ToList();

        _eventsManager.SubscribeEvents(domainEvents);

        await SaveChangesAsync();
    }
}
