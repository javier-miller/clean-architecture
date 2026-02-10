namespace Kensho.CleanArchitecture.Domain;

/// <summary>
/// Equatable
/// </summary>
/// <typeparam name="TType">The type of the type.</typeparam>
/// <seealso cref="IEquatable&lt;Equatable&lt;TType&gt;&gt;" />
public abstract class Equatable<TType> : IEquatable<Equatable<TType>>
    where TType : class
{
    /// <summary>
    /// Determines whether the specified <see cref="object" />, is equal to this instance.
    /// </summary>
    /// <param name="obj">The <see cref="object" /> to compare with this instance.</param>
    /// <returns>
    ///   <c>true</c> if the specified <see cref="object" /> is equal to this instance; otherwise, <c>false</c>.
    /// </returns>
    public override bool Equals(object? obj)
    {
        if (obj == null || obj.GetType() != GetType()) return false;

        var other = (Equatable<TType>)obj;

        return GetEqualityComponents().SequenceEqual(other.GetEqualityComponents());
    }

    /// <summary>
    /// Returns a hash code for this instance.
    /// </summary>
    /// <returns>
    /// A hash code for this instance, suitable for use in hashing algorithms and data structures like a hash table. 
    /// </returns>
    public override int GetHashCode()
    {
        return GetEqualityComponents()
            .Select(x => x != null ? x.GetHashCode() : 0)
            .Aggregate((x, y) => x ^ y);
    }

    /// <summary>
    /// Implements the operator ==.
    /// </summary>
    /// <param name="one">The one.</param>
    /// <param name="two">The two.</param>
    /// <returns>
    /// The result of the operator.
    /// </returns>
    public static bool operator ==(Equatable<TType>? one, Equatable<TType>? two) => EqualOperator(one, two);

    /// <summary>
    /// Implements the operator !=.
    /// </summary>
    /// <param name="one">The one.</param>
    /// <param name="two">The two.</param>
    /// <returns>
    /// The result of the operator.
    /// </returns>
    public static bool operator !=(Equatable<TType>? one, Equatable<TType>? two) => NotEqualOperator(one, two);

    /// <summary>
    /// Equals the operator.
    /// </summary>
    /// <param name="left">The left.</param>
    /// <param name="right">The right.</param>
    /// <returns></returns>
    protected static bool EqualOperator(Equatable<TType>? left, Equatable<TType>? right)
    {
        if (ReferenceEquals(left, null) ^ ReferenceEquals(right, null)) return false;
#pragma warning disable CS8602 // Dereference of a possibly null reference.
        return ReferenceEquals(left, right) || left.Equals(right);
#pragma warning restore CS8602 // Dereference of a possibly null reference.
    }

    /// <summary>
    /// Nots the equal operator.
    /// </summary>
    /// <param name="left">The left.</param>
    /// <param name="right">The right.</param>
    /// <returns></returns>
    protected static bool NotEqualOperator(Equatable<TType>? left, Equatable<TType>? right)
    {
        return !EqualOperator(left, right);
    }

    /// <summary>
    /// Gets the equality components.
    /// </summary>
    /// <returns></returns>
    protected abstract IEnumerable<object?> GetEqualityComponents();

    /// <summary>
    /// Indicates whether the current object is equal to another object of the same type.
    /// </summary>
    /// <param name="other">An object to compare with this object.</param>
    /// <returns>
    ///   <see langword="true" /> if the current object is equal to the <paramref name="other" /> parameter; otherwise, <see langword="false" />.
    /// </returns>
    public bool Equals(Equatable<TType>? other) => Equals((object?)other);
}
