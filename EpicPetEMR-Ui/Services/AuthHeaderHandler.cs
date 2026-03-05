namespace EpicPetEMR_Ui.Services;

public sealed class AuthHeaderHandler : DelegatingHandler
{
    private readonly TokenStore _tokenStore;

    public AuthHeaderHandler(TokenStore tokenStore)
    {
        _tokenStore = tokenStore;
    }

    protected override async Task<HttpResponseMessage> SendAsync(
        HttpRequestMessage request, CancellationToken cancellationToken)
    {
        var token = await _tokenStore.GetTokenAsync();

        if (!string.IsNullOrWhiteSpace(token))
            request.Headers.Authorization =
                new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
        request.Headers.Authorization =
       new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

        Console.WriteLine($"[AuthHeaderHandler] Full header: {request.Headers.Authorization}");
        return await base.SendAsync(request, cancellationToken);
    }
}