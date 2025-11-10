using System.Net.Http.Json;
using EpicPetEMR.Shared.Models;   // <- from Shared

namespace EpicPetEMR_Ui.Services;

public sealed class PetApi
{
    private readonly HttpClient _http;
    public PetApi(HttpClient http) => _http = http;

    public async Task<List<PetDto>> GetPetsAsync(CancellationToken ct = default)
        => await _http.GetFromJsonAsync<List<PetDto>>("demo/pets", ct) ?? new();

    public async Task UpdatePetAsync(PetDto pet,  CancellationToken ct = default)
    {
        var response = await _http.PutAsJsonAsync($"demo/pets/{pet.Id}", pet, ct);
        response.EnsureSuccessStatusCode();
    }

    public async Task AddPetAsync(PetDto pet, CancellationToken ct = default)
    {
        var response = await _http.PutAsJsonAsync($"demo/addpet/{pet.Id}", pet, ct);
        response.EnsureSuccessStatusCode();
    }
}
