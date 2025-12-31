using EpicPetEMR.Shared.Models;

namespace EpicPetEMR.Api.Models;

public class Pet
{
    public int Id { get; set; }

    public int? FamilyId { get; set; }  
    public Family? Family { get; set; }

    public string Name { get; set; } = "";
    public string ProfilePic { get; set; } = "";

    public Species Species { get; set; } = Species.Dog;
    public Sex Sex { get; set; } = Sex.Unknown;

    public string? Breed { get; set; }
    public DateOnly? DateOfBirth { get; set; }

    public decimal? Weight { get; set; }
    public WeightUnit? WeightUnit { get; set; }

    public List<Appointment> Appointments { get; set; } = new();
    public List<Attachment> Attachments { get; set; } = new();
}
