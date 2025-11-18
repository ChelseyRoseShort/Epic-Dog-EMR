using System.ComponentModel.DataAnnotations;
using EpicPetEMR.Shared.Models;
namespace EpicPetEMR.Api.Models;

public class Visit
{
    public int Id { get; set; }

    public int PetId { get; set; }
    public Pet Pet { get; set; } = default!;

    public DateOnly Date { get; set; }

    [MaxLength(160)]
    public string? ClinicName { get; set; }

    // simple SOAP-style notes
    public string? Subjective { get; set; }
    public string? Objective { get; set; }
    public string? Assessment { get; set; }
    public string? Plan { get; set; }

    // vitals / weight snapshot
    public decimal? Weight { get; set; }
    public Unit? WeightUnit { get; set; }
    public decimal? TemperatureC { get; set; }
}
