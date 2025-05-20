namespace CleanArchitecture.Domain.Extensions;

public static class QueryableExtensions
{
    public static bool IsNullOrEmpty<T>(this IQueryable<T> list)
    {
        return !(list != null && list.Any());
    }

    public static bool IsNotNullOrEmpty<T>(this IQueryable<T> list)
    {
        return list is not null && list.Any();
    }
}