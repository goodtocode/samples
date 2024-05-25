namespace Coffee.Presentation.Blazor.Common.Interfaces;

public interface IAuthenticatedUserService
{
    Task<string> GetLoggedInUserKeyAsync();
}