using System.Net.Http.Json;
using EpicPetEMR.Shared.Models;
using Microsoft.AspNetCore.Components.Forms;   // <- from Shared

namespace EpicPetEMR_Ui.Services;

public sealed class PetApi
{
    private readonly HttpClient _http;
    public PetApi(HttpClient http) => _http = http;

    public async Task<List<PetDto>> GetPetsAsync(CancellationToken ct = default)
        => await _http.GetFromJsonAsync<List<PetDto>>("demo/pets", ct) ?? new();

    public async Task UpdatePetAsync(PetDto pet,  CancellationToken ct = default)
    {
        var response = await _http.PutAsJsonAsync($"updatepet", pet, ct);
        response.EnsureSuccessStatusCode();
    }

    public async Task DeletePet(PetDto pet, CancellationToken ct = default)
    {
        using var request = new HttpRequestMessage(HttpMethod.Delete, "deletepet")
        {
            Content = JsonContent.Create(pet)
        };

        var response = await _http.SendAsync(request, ct);
        response.EnsureSuccessStatusCode();
    }
    // add photo function here
    public async Task<PetDto?> UploadPetPhoto(int Id, IBrowserFile file)
    {
        using var content = new MultipartFormDataContent();
        var stream = file.OpenReadStream(maxAllowedSize: 10 * 1024 * 1024); // 10 MB limit
        var fileContent = new StreamContent(stream);
        content.Add(fileContent, "file", file.Name);
        var response = await _http.PostAsync($"/uploadprofilepic/{Id}", content); 
        if (!response.IsSuccessStatusCode)
        {
            var error = await response.Content.ReadAsStringAsync();
            Console.WriteLine($"Error uploading photo: {error}");
            return null;
        }
        return await response.Content.ReadFromJsonAsync<PetDto>();
    }
    
    public async Task AddPetAsync(PetDto pet, CancellationToken ct = default)
    {
        var response = await _http.PutAsJsonAsync($"demo/addpet/{pet.Id}", pet, ct);
        response.EnsureSuccessStatusCode();
    }

    public async Task<PetDto?> AddPetAsync(PetDto newPet)
    {
        var response = await _http.PostAsJsonAsync("addpet", newPet);

        if (!response.IsSuccessStatusCode)
        {
            // you can throw or handle gracefully
            var error = await response.Content.ReadAsStringAsync();
            Console.WriteLine($"Error adding pet: {error}");
            return null;
        }

        return await response.Content.ReadFromJsonAsync<PetDto>();
    }


    // make single pet api call

    public async Task<PetDto> GetPetById(int id, CancellationToken ct = default)
        => await _http.GetFromJsonAsync<PetDto>($"getpet/{id}", ct);

}
