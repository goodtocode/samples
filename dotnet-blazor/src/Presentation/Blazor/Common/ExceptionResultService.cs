using System.Text;
using Coffee.Presentation.Blazor.Common.Interfaces;

namespace Coffee.Presentation.Blazor.Common;

public class ExceptionResultService : IExceptionResultService
{
    public Result<T> Response<T>(Exception e)
    {
        if (e is ApiException validationException)
            return Result.Fail<T>(CreateValidationErrorMessage(validationException));
        if (e.Message.Contains("Not Found"))
            return Result.Fail<T>("Not Found");
        if (e.Message.Equals("TypeError: Failed to fetch"))
            return Result.Fail<T>("API Service is Down, Try Again Later");
        return Result.Fail<T>("Error");
    }

    private string CreateValidationErrorMessage(ApiException validationException)
    {
        var errorSb = new StringBuilder();

        foreach (var error in validationException.Errors)
        {
            errorSb.AppendLine($"{error.Key} :");
            foreach (var errorValue in error.Value) errorSb.AppendLine($"{errorValue}");
        }

        return $"{validationException.Errors.First().Key} Error: {errorSb}";
    }
}