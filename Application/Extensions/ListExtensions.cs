namespace CleanArchitecture.Application.Extensions
{
    public static class ListExtensions
    {
        public static bool IsNotEmpty<T>(this IList<T> list)
        {
            return list != null && list.Count > 0;
        }
    }
}
