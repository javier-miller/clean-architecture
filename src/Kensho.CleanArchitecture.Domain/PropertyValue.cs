namespace Kensho.CleanArchitecture.Domain;

/// <summary>
/// Property Value
/// </summary>
/// <typeparam name="TType">The type of the type.</typeparam>
/// <seealso cref="ValueObject" />
public class PropertyValue<TType> : ValueObject
{
    /// <summary>
    /// Prevents a default instance of the <see cref="PropertyValue{TType}"/> class from being created.
    /// </summary>
    protected PropertyValue() { }

    /// <summary>
    /// Initializes a new instance of the <see cref="PropertyValue{TType}"/> class.
    /// </summary>
    /// <param name="value">The value.</param>
    public PropertyValue(TType value)
    {
        Value = value;
    }

    /// <summary>
    /// Gets the value.
    /// </summary>
    /// <value>
    /// The value.
    /// </value>
    public TType? Value { get; private set; }

    /// <summary>
    /// Gets the equality components.
    /// </summary>
    /// <returns></returns>
    protected override IEnumerable<object?> GetEqualityComponents()
    {
        yield return Value;
    }
}
