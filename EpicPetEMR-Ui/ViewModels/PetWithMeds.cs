using EpicPetEMR.Shared.Models;

namespace EpicPetEMR_Ui.ViewModels
{
    public class PetWithMedsVm
    {
        public PetDto Pet { get; set; } = new();
        public List<MedicationDto> Medications { get; set; } = new();
    }
}

