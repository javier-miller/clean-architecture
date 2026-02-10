namespace Kensho.CleanArchitecture.Domain;

/// <summary>
/// Aggregate
/// </summary>
/// <typeparam name="TId">The type of the identifier.</typeparam>
/// <typeparam name="TIdType">The type of the identifier type.</typeparam>
/// <seealso cref="Entity&lt;TId&gt;" />
public abstract class Aggregate<TId, TIdType> : Entity<TId>
    where TIdType : AggregateId<TId>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="Aggregate{TId, TIdType}"/> class.
    /// </summary>
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    protected Aggregate() : base() { }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

    /// <summary>
    /// Initializes a new instance of the <see cref="Aggregate{TId, TIdType}"/> class.
    /// </summary>
    /// <param name="id">The identifier.</param>
    protected Aggregate(TIdType id) : base(id.Value)
    {
        Id = id;

        //Created = DateTimeOffset.Now;
        //Updated = Created;
    }

    /// <summary>
    /// Gets or sets the identifier.
    /// </summary>
    /// <value>
    /// The identifier.
    /// </value>
    public new TIdType Id { get; protected set; }

    ///// <summary>
    ///// Gets the created.
    ///// </summary>
    ///// <value>
    ///// The created.
    ///// </value>
    //public DateTimeOffset Created { get; private set; }

    ///// <summary>
    ///// Gets the updated.
    ///// </summary>
    ///// <value>
    ///// The updated.
    ///// </value>
    //public DateTimeOffset Updated { get; private set; }

    ///// <summary>
    ///// Updates this instance.
    ///// </summary>
    ///// <returns></returns>
    //protected Aggregate<TId, TIdType> Update()
    //{
    //    Updated = DateTimeOffset.Now;

    //    return this;
    //}

    /// <summary>
    /// Gets the equality components.
    /// </summary>
    /// <returns></returns>
    protected override IEnumerable<object?> GetEqualityComponents()
    {
        yield return Id;
        //yield return Created;
        //yield return Updated;
    }
}
