using EpicPetEMR.Shared.Models;
using EpicPetEMR_Ui.ViewModels;
using Microsoft.AspNetCore.Components.Forms;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using static EpicPetEMR_Ui.Pages.MAR;

namespace EpicPetEMR_Ui.Services;

using System.Text.Json;
using System.Text.Json.Serialization;

public sealed class PetApi
{
    private readonly HttpClient _http;
    public PetApi(HttpClient http) => _http = http;

    // -------------------------
    // Pets
    // -------------------------
    public async Task<List<PetWithMedsVm>> GetPetsAsync(bool includeMeds = false, CancellationToken ct = default)
    => await _http.GetFromJsonAsync<List<PetWithMedsVm>>(
        $"api/pet/demo?includeMeds={includeMeds.ToString().ToLower()}",
        _jsonOptions,
        ct) ?? new();

    public async Task<List<PetDto>> GetAllPetsAsync(CancellationToken ct = default, bool includeMeds = false)
        => await _http.GetFromJsonAsync<List<PetDto>>($"api/pet/demo?includeMeds={includeMeds.ToString().ToLower()}", _jsonOptions, ct) ?? new();

    public async Task<PetDto> GetPetById(int id, CancellationToken ct = default)
        => await _http.GetFromJsonAsync<PetDto>($"api/pet/{id}", _jsonOptions, ct);

    public async Task<PetDto?> AddPetAsync(PetDto newPet)
    {
        var response = await _http.PostAsJsonAsync("api/pet", newPet, _jsonOptions);
        if (!response.IsSuccessStatusCode)
        {
            Console.WriteLine($"Error adding pet: {await response.Content.ReadAsStringAsync()}");
            return null;
        }
        return await response.Content.ReadFromJsonAsync<PetDto>(_jsonOptions);
    }

    public async Task UpdatePetAsync(PetDto pet, CancellationToken ct = default)
    {
        var response = await _http.PutAsJsonAsync("api/pet", pet, _jsonOptions, ct);
        response.EnsureSuccessStatusCode();
    }

    public async Task DeletePet(PetDto pet, CancellationToken ct = default)
    {
        using var request = new HttpRequestMessage(HttpMethod.Delete, "api/pet")
        {
            Content = JsonContent.Create(pet, options: _jsonOptions)
        };
        var response = await _http.SendAsync(request, ct);
        response.EnsureSuccessStatusCode();
    }

    public async Task<PetDto?> UploadPetPhoto(int id, IBrowserFile file)
    {
        using var content = new MultipartFormDataContent();
        var stream = file.OpenReadStream(maxAllowedSize: 100 * 1024 * 1024);
        content.Add(new StreamContent(stream), "file", file.Name);

        var response = await _http.PostAsync($"api/pet/{id}/profilepic", content);
        if (!response.IsSuccessStatusCode)
        {
            Console.WriteLine($"Error uploading photo: {await response.Content.ReadAsStringAsync()}");
            return null;
        }
        return await response.Content.ReadFromJsonAsync<PetDto>(_jsonOptions);
    }

    // -------------------------
    // Medications
    // -------------------------

    public async Task<List<MedicationDto>> GetMedsById(int id, CancellationToken ct = default)
        => await _http.GetFromJsonAsync<List<MedicationDto>>($"api/medication/bypet/{id}", _jsonOptions, ct);

    public async Task<MedicationDto> GetMedById(int id, CancellationToken ct = default)
        => await _http.GetFromJsonAsync<MedicationDto>($"api/medication/{id}", _jsonOptions, ct);

    public async Task<MedicationDto> AddMedAsync(MedicationDto newMedication)
    {
        var response = await _http.PostAsJsonAsync("api/medication", newMedication, _jsonOptions);
        if (!response.IsSuccessStatusCode)
        {
            Console.WriteLine($"Error adding medication: {await response.Content.ReadAsStringAsync()}");
            return null;
        }
        return await response.Content.ReadFromJsonAsync<MedicationDto>(_jsonOptions);
    }

    public async Task DeleteMed(MedicationDto med, CancellationToken ct = default)
    {
        using var request = new HttpRequestMessage(HttpMethod.Delete, "api/medication")
        {
            Content = JsonContent.Create(med, options: _jsonOptions)
        };
        var response = await _http.SendAsync(request, ct);
        response.EnsureSuccessStatusCode();
    }

    // -------------------------
    // Families
    // -------------------------

    public async Task<List<FamilyDto>> GetMyFamiliesAsync()
        => await _http.GetFromJsonAsync<List<FamilyDto>>("api/family/with-pets", _jsonOptions) ?? new();

    // -------------------------
    // Avatars
    // -------------------------

    public async Task<List<AvatarDto>> GetAvatarById(int petId, CancellationToken ct = default)
        => await _http.GetFromJsonAsync<List<AvatarDto>>($"api/pets/{petId}/avatars", _jsonOptions, ct);

    public async Task<AvatarDto?> AddAvatarAsync(int petId, CreateAvatarRequest req)
    {
        var response = await _http.PostAsJsonAsync($"api/pets/{petId}/avatars", req, _jsonOptions);
        if (!response.IsSuccessStatusCode)
        {
            Console.WriteLine($"Error adding avatar: {await response.Content.ReadAsStringAsync()}");
            return null;
        }
        return await response.Content.ReadFromJsonAsync<AvatarDto>(_jsonOptions);
    }

    public async Task UpdateAvatarAsync(int petId, UpdateAvatarRequest finding, CancellationToken ct = default)
    {
        var response = await _http.PutAsJsonAsync($"api/pets/{petId}/avatars", finding, _jsonOptions, ct);
        response.EnsureSuccessStatusCode();
    }

    public async Task DeleteAvatar(int petId, int id, CancellationToken ct = default)
    {
        var response = await _http.DeleteAsync($"api/pets/{petId}/avatars/{id}", ct);
        response.EnsureSuccessStatusCode();
    }

    // -------------------------
    // Medical History
    // -------------------------

    public async Task<List<PetMedicalHistoryDto>> GetMedicalHistoryById(int petId, CancellationToken ct = default)
        => await _http.GetFromJsonAsync<List<PetMedicalHistoryDto>>($"api/pets/{petId}/medical-history", _jsonOptions, ct);

    public async Task<PetMedicalHistoryDto> AddMedicalHistoryAsync(PetMedicalHistoryDto newPetMedicalHistory)
    {
        var petId = newPetMedicalHistory.PetId;
        var response = await _http.PostAsJsonAsync($"api/pets/{petId}/medical-history", newPetMedicalHistory, _jsonOptions);
        if (!response.IsSuccessStatusCode)
        {
            Console.WriteLine($"Error adding medical history: {await response.Content.ReadAsStringAsync()}");
            return null;
        }
        return await response.Content.ReadFromJsonAsync<PetMedicalHistoryDto>(_jsonOptions);
    }

    // -------------------------
    // Oh No Events
    // -------------------------

    public async Task<List<OhNoEventDto>> GetOhNoEventsById(int petId, CancellationToken ct = default)
        => await _http.GetFromJsonAsync<List<OhNoEventDto>>($"api/{petId}/ohnoevents", _jsonOptions, ct);

    public async Task<OhNoEventDto> AddOhNoEventAsync(OhNoEventDto model)
    {
        var response = await _http.PostAsJsonAsync($"api/{model.PetId}/ohnoevents", model, _jsonOptions);
        if (!response.IsSuccessStatusCode)
        {
            Console.WriteLine($"Error adding Oh No Event: {await response.Content.ReadAsStringAsync()}");
            return null;
        }
        return await response.Content.ReadFromJsonAsync<OhNoEventDto>(_jsonOptions);
    }

    // -------------------------
    // Documents
    // -------------------------

    public async Task<List<PetDocumentDto>> GetPetDocumentsAsync(int petId, CancellationToken ct = default)
        => await _http.GetFromJsonAsync<List<PetDocumentDto>>($"api/documents/{petId}", _jsonOptions, ct) ?? new();

    public async Task<PetDocumentDto> UploadPetDocumentAsync(int petId, IBrowserFile file, string name, DocumentType type, int? vetTripId = null)
    {
        const long maxFileSize = 20 * 1024 * 1024;
        using var content = new MultipartFormDataContent();

        var fileContent = new StreamContent(file.OpenReadStream(maxFileSize));
        fileContent.Headers.ContentType = new MediaTypeHeaderValue(file.ContentType ?? "application/octet-stream");
        content.Add(fileContent, "file", file.Name);
        content.Add(new StringContent(name), "name");
        content.Add(new StringContent(type.ToString()), "type");
        if (vetTripId.HasValue)
            content.Add(new StringContent(vetTripId.Value.ToString()), "vetTripId");

        var response = await _http.PostAsync($"api/documents/{petId}/documents", content);
        response.EnsureSuccessStatusCode();

        return (await response.Content.ReadFromJsonAsync<PetDocumentDto>(_jsonOptions))!;
    }

    // -------------------------
    // Vet Trips
    // -------------------------

    public async Task<List<VetTripDto>> GetVetTripsTodayAsync(CancellationToken ct = default)
        => await _http.GetFromJsonAsync<List<VetTripDto>>("api/vettrips/today", _jsonOptions, ct) ?? new();

    public async Task<List<VetTripDto>> GetVetTripsByPet(int petId, CancellationToken ct = default)
        => await _http.GetFromJsonAsync<List<VetTripDto>>($"api/vettrips/by-pet/{petId}", _jsonOptions, ct);

    public async Task<VetTripDto?> AddVetTripAsync(VetTripDto dto)
    {
        var response = await _http.PostAsJsonAsync("api/vettrips", dto, _jsonOptions);
        response.EnsureSuccessStatusCode();
        return await response.Content.ReadFromJsonAsync<VetTripDto>(_jsonOptions);
    }

    // -------------------------
    // MAR
    // -------------------------

    public async Task<List<MARHistoryDto>> GetMARHistoryAsync()
        => await _http.GetFromJsonAsync<List<MARHistoryDto>>("api/mar/history", _jsonOptions) ?? new();

    public async Task<List<MARHistoryDto>> GetMarHistoryForDateAsync(DateOnly date, int? petId = null)
    {
        var isoDate = date.ToString("yyyy-MM-dd");
        var url = petId.HasValue
            ? $"api/mar/history/bydate?date={isoDate}&petId={petId.Value}"
            : $"api/mar/history/bydate?date={isoDate}";

        return await _http.GetFromJsonAsync<List<MARHistoryDto>>(url, _jsonOptions) ?? new();
    }

    public async Task MarkGivenAsync(List<SelectedMed> selected)
    {
        var payload = selected.Select(x => new MARHistoryDto
        {
            PetId = x.PetId,
            MedId = x.MedId,
            Hour = x.Hour,
            Action = MedicationAction.Given,
            TimeRecorded = DateTime.UtcNow
        }).ToList();

        await _http.PostAsJsonAsync("api/mar/given", payload, _jsonOptions);
    }

    public async Task MarkHeldAsync(List<SelectedMed> selected, string reason)
    {
        var payload = selected.Select(x => new MARHistoryDto
        {
            PetId = x.PetId,
            MedId = x.MedId,
            Hour = x.Hour,
            Action = MedicationAction.Held,
            Reason = reason
        }).ToList();

        await _http.PostAsJsonAsync("api/mar/given", payload, _jsonOptions);
    }

    // -------------------------

    private static readonly JsonSerializerOptions _jsonOptions = new()
    {
        Converters = { new JsonStringEnumConverter() },
        PropertyNameCaseInsensitive = true
    };
}