namespace CleanArchitecture.Domain.DTO.Request
{
    public record LoginRequest(string Username, string Password);
    public record PageRequest(int PageNumber, int PageSize);
}
