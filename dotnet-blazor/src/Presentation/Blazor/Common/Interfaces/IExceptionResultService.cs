namespace Coffee.Presentation.Blazor.Common.Interfaces;

public interface IExceptionResultService
{
    Result<T> Response<T>(Exception e);
}