namespace CleanArchitecture.Domain.Utils
{
    public record Page(int PageNumber = 1, int PageSize = 10);

    /// <summary>
    /// Provides extension methods on a <see cref="Page"/> instance.
    /// </summary>
    public static class PageExtension
    {
        /// <summary>
        /// Calculates how many entries need to be skipped for pagination.
        /// </summary>
        /// <param name="page">A <see cref="Page"/> instance.</param>
        /// <returns>Returns the number of entries that needs to be skipped to get the correct page context.</returns>
        public static int LineToSkip(this Page page)
        {
            return page.PageNumber > 1 ? (page.PageNumber - 1) * page.PageSize : 0;
        }
    }
}
