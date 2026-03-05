using System.Net.Http.Json;
using EpicPetEMR.Shared.Models;

namespace EpicPetEMR_Ui.Services;

public sealed class FamilyApi
{
    private readonly HttpClient _http;
    public FamilyApi(HttpClient http) => _http = http;

    public async Task<List<FamilyDto>> GetMyFamiliesAsync()
    {
        var res = await _http.GetAsync("api/families");
        if (!res.IsSuccessStatusCode)
            throw new Exception(await res.Content.ReadAsStringAsync());
        return await res.Content.ReadFromJsonAsync<List<FamilyDto>>() ?? new();
    }

    public async Task<FamilyDto> CreateFamilyAsync(string name)
    {
        var res = await _http.PostAsJsonAsync("api/families", new { Name = name });
        if (!res.IsSuccessStatusCode)
            throw new Exception(await res.Content.ReadAsStringAsync());
        return await res.Content.ReadFromJsonAsync<FamilyDto>()
            ?? throw new Exception("Empty response from server.");
    }

    public async Task<UserLookupDto?> LookupUserByEmailAsync(string email)
    {
        var res = await _http.GetAsync($"api/users/lookup?email={Uri.EscapeDataString(email)}");
        if (res.StatusCode == System.Net.HttpStatusCode.NotFound)
            return null;
        if (!res.IsSuccessStatusCode)
            throw new Exception(await res.Content.ReadAsStringAsync());
        return await res.Content.ReadFromJsonAsync<UserLookupDto>();
    }

    public async Task AddMemberAsync(int familyId, int userId, string role = "Member")
    {
        // FamilyRole enum: Owner=0, Admin=1, Member=2, ReadOnly=3
        var roleInt = role switch
        {
            "Admin" => 1,
            "ReadOnly" => 3,
            _ => 2  // default Member
        };

        var res = await _http.PostAsJsonAsync($"api/families/{familyId}/members",
            new { UserId = userId, Role = roleInt });

        if (!res.IsSuccessStatusCode)
            throw new Exception(await res.Content.ReadAsStringAsync());
    }
}