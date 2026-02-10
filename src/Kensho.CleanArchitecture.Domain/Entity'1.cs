namespace Kensho.CleanArchitecture.Domain;

/// <summary>
/// Entity
/// </summary>
/// <typeparam name="TId">The type of the identifier.</typeparam>
/// <seealso cref="Equatable&lt;Entity&lt;TId&gt;&gt;" />
/// <seealso cref="Equatable&lt;Entity&gt;" />
public abstract class Entity<TId> : Entity
{

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    protected Entity() { }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

    /// <summary>
    /// Initializes a new instance of the <see cref="Entity{TId}"/> class.
    /// </summary>
    /// <param name="id">The identifier.</param>
    protected Entity(TId id) : base()
    {
        Id = id;
    }

    /// <summary>
    /// Gets or sets the identifier.
    /// </summary>
    /// <value>
    /// The identifier.
    /// </value>
    public TId Id { get; protected set; }

    /// <summary>
    /// Gets the equality components.
    /// </summary>
    /// <returns></returns>
    protected override IEnumerable<object?> GetEqualityComponents()
    {
        yield return Id;
    }
}
