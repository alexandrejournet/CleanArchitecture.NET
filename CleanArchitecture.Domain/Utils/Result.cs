namespace CleanArchitecture.Domain.Utils;

public class Result<TSuccess, TError>
{
    public TSuccess Success { get; }
    public TError Error { get; }
    public bool IsSuccess { get; }

    protected Result(TSuccess success, TError error, bool isSuccess)
    {
        Success = success;
        Error = error;
        IsSuccess = isSuccess;
    }

    public static Result<TSuccess, TError> Ok(TSuccess success) => new(success, default, true);
    public static Result<TSuccess, TError> Err(TError error) => new(default, error, false);
}

public static class ResultExtensions
{
    public static TResult Match<TSuccess, TError, TResult>(
        this Result<TSuccess, TError> result,
        Func<TSuccess, TResult> onSuccess,
        Func<TError, TResult> onError)
    {
        return result.IsSuccess ? onSuccess(result.Success) : onError(result.Error);
    }
}

public static class MatchExtensions
{
    // Pour les types classes
    public static TResult Some<T, TResult>(this T? value, Func<T, TResult> onSome, Func<TResult> onNone) where T : class
    {
        return value != null ? onSome(value) : onNone();
    }
    public static async Task<TResult> Some<T, TResult>(this T? value, Func<T, Task<TResult>> onSome, Func<TResult> onNone) where T : class
    {
        return value != null ? await onSome(value) : onNone();
    }

    public static async Task<TResult> Some<T, TResult>(this T? value, Func<T, TResult> onSome, Func<Task<TResult>> onNone) where T : class
    {
        return value != null ? onSome(value) : await onNone();
    }

    public static async Task<TResult> Some<T, TResult>(this T? value, Func<T, Task<TResult>> onSome, Func<Task<TResult>> onNone) where T : class
    {
        return value != null ? await onSome(value) : await onNone();
    }


    // Pour les types nullables
    public static TResult Some<T, TResult>(this T? value, Func<T, TResult> onSome, Func<TResult> onNone) where T : struct
    {
        return value.HasValue ? onSome(value.Value) : onNone();
    }

    public static async Task<TResult> Some<T, TResult>(this T? value, Func<T, Task<TResult>> onSome, Func<TResult> onNone) where T : struct
    {
        return value.HasValue ? await onSome(value.Value) : onNone();
    }

    public static async Task<TResult> Some<T, TResult>(this T? value, Func<T, TResult> onSome, Func<Task<TResult>> onNone) where T : struct
    {
        return value.HasValue ? onSome(value.Value) : await onNone();
    }

    public static async Task<TResult> Some<T, TResult>(this T? value, Func<T, Task<TResult>> onSome, Func<Task<TResult>> onNone) where T : struct
    {
        return value.HasValue ? await onSome(value.Value) : await onNone();
    }
}