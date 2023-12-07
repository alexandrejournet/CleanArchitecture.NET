using Microsoft.AspNetCore.Http;
using System.Runtime.Serialization;

namespace CleanArchitecture.Domain.Exceptions;

[Serializable]
public class ProjectException : Exception
{


    public int? StatusCode { get; set; }

    protected ProjectException(SerializationInfo info, StreamingContext context) : base(info, context)
    {
    }

    public ProjectException(string? message)
        : base(message)
    {
    }

    public ProjectException(int statusCode, string? message)
        : base(message)
    {
        StatusCode = statusCode;
    }

    public ProjectException(int statusCode, string? message, Exception? innerException)
        : base(message, innerException)
    {
        StatusCode = statusCode;
    }

    public ProjectException(string? message, Exception innerException)
        : base(message, innerException)
    {
    }

    public static ProjectException Format(ProjectErrorEnum projectErrorEnum, params object[] values)
    {
        return new ProjectException(string.Format(projectErrorEnum.ShowError(), values));
    }

    public static ProjectException Format(int statusCode, ProjectErrorEnum projectErrorEnum, params object[] values)
    {
        return new ProjectException(statusCode, string.Format(projectErrorEnum.ShowError(), values));
    }

    public static ProjectException Format(Exception ex, ProjectErrorEnum projectErrorEnum, params object[] values)
    {
        return new ProjectException(string.Format(projectErrorEnum.ShowError(), values), ex);
    }

    public static ProjectException Format(Exception ex, int statusCode, ProjectErrorEnum projectErrorEnum,
        params object[] values)
    {
        return new ProjectException(statusCode, string.Format(projectErrorEnum.ShowError(), values), ex);
    }

    public static ProjectException Error400(params object[] values)
    {
        return values.Length == 0
            ? new ProjectException(StatusCodes.Status400BadRequest,
                string.Format(ProjectErrorEnum.PROJECT_400.ShowError(), values))
            : new ProjectException(StatusCodes.Status400BadRequest,
                string.Format(ProjectErrorEnum.PROJECT_400_WITH_CAUSE.ShowError(), values));
    }

    public static ProjectException Error401(params object[] values)
    {
        return values.Length == 0
            ? new ProjectException(StatusCodes.Status401Unauthorized,
                string.Format(ProjectErrorEnum.PROJECT_401.ShowError(), values))
            : new ProjectException(StatusCodes.Status401Unauthorized,
                string.Format(ProjectErrorEnum.PROJECT_401_WITH_CAUSE.ShowError(), values));
    }

    public static ProjectException Error403(params object[] values)
    {
        return values.Length == 0
            ? new ProjectException(StatusCodes.Status403Forbidden,
                string.Format(ProjectErrorEnum.PROJECT_403.ShowError(), values))
            : new ProjectException(StatusCodes.Status403Forbidden,
                string.Format(ProjectErrorEnum.PROJECT_403_WITH_CAUSE.ShowError(), values));
    }

    public static ProjectException Error404(params object[] values)
    {
        return values.Length == 0
            ? new ProjectException(StatusCodes.Status404NotFound,
                string.Format(ProjectErrorEnum.PROJECT_404.ShowError(), values))
            : new ProjectException(StatusCodes.Status404NotFound,
                string.Format(ProjectErrorEnum.PROJECT_404_WITH_CAUSE.ShowError(), values));
    }

    public static ProjectException Error422(params object[] values)
    {
        return values.Length == 0
            ? new ProjectException(StatusCodes.Status422UnprocessableEntity,
                string.Format(ProjectErrorEnum.PROJECT_422.ShowError(), values))
            : new ProjectException(StatusCodes.Status422UnprocessableEntity,
                string.Format(ProjectErrorEnum.PROJECT_422_WITH_CAUSE.ShowError(), values));
    }

    public static ProjectException Error500(params object[] values)
    {
        return values.Length == 0
            ? new ProjectException(StatusCodes.Status500InternalServerError,
                string.Format(ProjectErrorEnum.PROJECT_500.ShowError(), values))
            : new ProjectException(StatusCodes.Status500InternalServerError,
                string.Format(ProjectErrorEnum.PROJECT_500_WITH_CAUSE.ShowError(), values));
    }
}