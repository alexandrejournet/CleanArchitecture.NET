namespace CleanArchitecture.Domain.Extensions;

public static class EnumerableExtensions
{

    public static bool IsNullOrEmpty<T>(this IEnumerable<T> list)
    {
        return !(list != null && list.Any());
    }

    public static bool IsNotNullOrEmpty<T>(this IEnumerable<T> list)
    {
        return list is not null && list.Any();
    }
}