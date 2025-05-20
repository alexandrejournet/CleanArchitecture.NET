namespace CleanArchitecture.Domain.Utils;

public record Error(string Code, string Message)
{
    public static Error None = new(string.Empty, string.Empty);
    public static Error NullValue = new("Error.NullValue", "Null value was provided.");

    // Auth errors
    public static Error InvalidCredentials => new("Auth.InvalidCredentials", "Invalid credentials.");
    public static Error AlreadyExistsUsername => new("Auth.AlreadyExistsUsername", "Username already exists.");
    public static Error AlreadyExistsEmail => new("Auth.AlreadyExistsUsername", "Email already exists.");
    public static Error InvalidUser => new("Auth.InvalidUser", "Invalid user data.");
}
