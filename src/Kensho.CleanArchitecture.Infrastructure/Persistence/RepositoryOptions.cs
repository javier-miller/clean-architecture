namespace Kensho.CleanArchitecture.Infrastructure.Persistence;

/// <summary>
/// Repository Options
/// </summary>
public class RepositoryOptions
{
    public static readonly RepositoryOptions Empty = new RepositoryOptions(false);

    /// <summary>
    /// Initializes a new instance of the <see cref="RepositoryOptions"/> class.
    /// </summary>
    /// <param name="asNoTracking">if set to <c>true</c> [as no tracking].</param>
    public RepositoryOptions(bool asNoTracking)
    {
        AsNoTracking = asNoTracking;
    }

    /// <summary>
    /// Gets a value indicating whether [as no tracking].
    /// </summary>
    /// <value>
    ///   <c>true</c> if [as no tracking]; otherwise, <c>false</c>.
    /// </value>
    public bool AsNoTracking { get; }
}
