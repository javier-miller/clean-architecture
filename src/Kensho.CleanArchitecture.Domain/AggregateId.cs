namespace Kensho.CleanArchitecture.Domain;

/// <summary>
/// Aggregate Identifier
/// </summary>
/// <typeparam name="TId">The type of the identifier.</typeparam>
/// <seealso cref="ValueObject" />
public abstract class AggregateId<TId> : ValueObject
{
    /// <summary>
    /// Initializes a new instance of the <see cref="AggregateId{TId}"/> class.
    /// </summary>
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    protected AggregateId() { }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

    /// <summary>
    /// Initializes a new instance of the <see cref="AggregateId{TId}"/> class.
    /// </summary>
    /// <param name="value">The value.</param>
    public AggregateId(TId value)
    {
        Value = value;
    }

    /// <summary>
    /// Gets the value.
    /// </summary>
    /// <value>
    /// The value.
    /// </value>
    public TId Value { get; protected set; }

    /// <summary>
    /// Gets the equality components.
    /// </summary>
    /// <returns></returns>
    protected override IEnumerable<object?> GetEqualityComponents()
    {
        yield return Value;
    }
}
