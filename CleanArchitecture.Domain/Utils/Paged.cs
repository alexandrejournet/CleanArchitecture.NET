namespace CleanArchitecture.Domain.Utils
{
    public class Paged<T>
    {
        public ICollection<T>? Items { get; set; }
        public int Count { get; set; }
    }
}
