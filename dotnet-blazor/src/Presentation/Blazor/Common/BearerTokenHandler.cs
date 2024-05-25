using System.Net.Http.Headers;
using Microsoft.AspNetCore.Components.WebAssembly.Authentication;

namespace Coffee.Presentation.Blazor.Common;

public class BearerTokenHandler : DelegatingHandler
{
    private readonly IAccessTokenProvider _tokenService;

    public BearerTokenHandler(IAccessTokenProvider tokenService)
    {
        _tokenService = tokenService;
    }

    protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request,
        CancellationToken cancellationToken)
    {
        var accessTokenResult = await _tokenService.RequestAccessToken();
        if (accessTokenResult.TryGetToken(out var token))
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token.Value);
        return await base.SendAsync(request, cancellationToken);
    }
}