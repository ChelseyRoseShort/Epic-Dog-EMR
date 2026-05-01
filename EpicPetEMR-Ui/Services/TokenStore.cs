using Microsoft.JSInterop;
namespace EpicPetEMR_Ui.Services;

public sealed class TokenStore
{
    private const string TokenKey = "epicpetemr_token";
    private const string EmailKey = "epicpetemr_email";
    private readonly IJSRuntime _js;
    public TokenStore(IJSRuntime js) => _js = js;
    public ValueTask SetTokenAsync(string token) =>
        _js.InvokeVoidAsync("localStorage.setItem", TokenKey, token);
    public async ValueTask<string?> GetTokenAsync()
    {
        var raw = await _js.InvokeAsync<string?>("localStorage.getItem", TokenKey);
        return raw?.Trim('\'').Trim('"');
    }
    public ValueTask SetEmailAsync(string email) =>
        _js.InvokeVoidAsync("localStorage.setItem", EmailKey, email);
    public ValueTask<string?> GetEmailAsync() =>
        _js.InvokeAsync<string?>("localStorage.getItem", EmailKey);
    public async ValueTask ClearAsync()
    {
        await _js.InvokeVoidAsync("localStorage.removeItem", TokenKey);
        await _js.InvokeVoidAsync("localStorage.removeItem", EmailKey);
    }
    public ValueTask SetAsync(string token) => SetTokenAsync(token);
    public ValueTask<string?> GetAsync() => GetTokenAsync();
}