namespace CleanArchitecture.Domain.Request
{
    public record LoginRequest(string Username, string Password);
    public record PageRequest(int PageNumber, int PageSize);
}
