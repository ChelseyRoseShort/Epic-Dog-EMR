namespace EpicPetEMR_Ui.Services;
using Microsoft.AspNetCore.Components;
using EpicPetEMR_Ui.Services;

public class AuthRedirectHandler : DelegatingHandler
{
    private readonly NavigationManager _nav;
    private readonly TokenStore _tokens;

    public AuthRedirectHandler(NavigationManager nav, TokenStore tokens)
    {
        _nav = nav;
        _tokens = tokens;
    }
    protected override async Task<HttpResponseMessage> SendAsync(
        HttpRequestMessage request, CancellationToken cancellationToken)
    {
        var response = await base.SendAsync(request, cancellationToken);

        if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
        {
            _tokens.ClearAsync();
            _nav.NavigateTo("/login", forceLoad: true);
        }
        return response;
    }
}
