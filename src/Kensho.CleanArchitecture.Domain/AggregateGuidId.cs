namespace Kensho.CleanArchitecture.Domain;

/// <summary>
/// Aggregate Guid Identifier
/// </summary>
/// <seealso cref="AggregateId&lt;Guid&gt;" />
public abstract class AggregateGuidId : AggregateId<Guid>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="AggregateGuidId"/> class.
    /// </summary>
    public AggregateGuidId() { }

    /// <summary>
    /// Initializes a new instance of the <see cref="AggregateGuidId"/> class.
    /// </summary>
    /// <param name="id"></param>
    public AggregateGuidId(Guid id) : base(id) { }

    /// <summary>
    /// Creates the specified value.
    /// </summary>
    /// <typeparam name="TType">The type of the type.</typeparam>
    /// <param name="value">The value.</param>
    /// <returns></returns>
    public static TType Create<TType>(Guid value) where TType : AggregateGuidId, new() => new TType()
    {
        Value = value
    };

    /// <summary>
    /// Creates this instance.
    /// </summary>
    /// <typeparam name="TType">The type of the type.</typeparam>
    /// <returns></returns>
    public static TType Create<TType>() where TType : AggregateGuidId, new() => new TType()
    {
        Value = Guid.NewGuid()
    };
}