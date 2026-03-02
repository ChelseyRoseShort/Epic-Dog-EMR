using System.Net.Http.Json;
using EpicPetEMR.Shared.Models.Auth;

namespace EpicPetEMR_Ui.Services;

public sealed class AuthApi
{
    private readonly HttpClient _http;
    public AuthApi(HttpClient http) => _http = http;

    public async Task<AuthResponse> RegisterAsync(RegisterRequest req)
    {
        var res = await _http.PostAsJsonAsync("api/auth/register", req);
        var body = await res.Content.ReadFromJsonAsync<AuthResponse>();
        if (!res.IsSuccessStatusCode || body is null)
            throw new Exception(await res.Content.ReadAsStringAsync());
        return body;
    }

    public async Task<AuthResponse> LoginAsync(LoginRequest req)
    {
        var res = await _http.PostAsJsonAsync("api/auth/login", req);
        var body = await res.Content.ReadFromJsonAsync<AuthResponse>();
        if (!res.IsSuccessStatusCode || body is null)
            throw new Exception(await res.Content.ReadAsStringAsync());
        return body;
    }
}
