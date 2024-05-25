using Microsoft.AspNetCore.Components.Authorization;
using Coffee.Presentation.Blazor.Common.Interfaces;

namespace Coffee.Presentation.Blazor.Common;

public class AuthenticatedUserService : IAuthenticatedUserService
{
    private readonly AuthenticationStateProvider _authenticationStateProvider;


    public AuthenticatedUserService(AuthenticationStateProvider authenticationStateProvider)
    {
        _authenticationStateProvider = authenticationStateProvider;
    }

    public async Task<string> GetLoggedInUserKeyAsync()
    {
        var authState = await _authenticationStateProvider.GetAuthenticationStateAsync();

        var userKey = authState.User.Claims.FirstOrDefault(x => x.Type == "oid")?.Value ?? "";

        return userKey;
    }
}