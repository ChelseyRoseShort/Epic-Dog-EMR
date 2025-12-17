using EpicPetEMR.Shared.Models;

namespace EpicPetEMR.Api.Models;

public class OhNoEvent
{
    public int Id { get; set; }

    public int PetId { get; set; }
    public Pet Pet { get; set; } = null!;

    public int? VetTripId { get; set; }
    public VetTrip? VetTrip { get; set; }

    public int? BrainTaskId { get; set; }

    public OhNoEventType Type { get; set; }

    public DateTime OccurredAt { get; set; } = DateTime.UtcNow;

    public string? Notes { get; set; }

    // ----------------------------
    // Generic "Oh NO" flags
    

    public bool? EventCollapseOrFainting { get; set; }
    public bool? EventDuringOrAfterExercise { get; set; }
    public int? EventHeartRateIfKnown { get; set; }
    public bool? EventBlueOrPaleGums { get; set; }

    // ----------------------------
    // Seizure-specific fields
    

    public int? SeizureDurationSeconds { get; set; }
    public bool? SeizureWasGeneralized { get; set; }
    public bool? SeizureLossOfConsciousness { get; set; }
    public bool? SeizureUrination { get; set; }
    public bool? SeizureIncontinentBM { get; set; }
    public bool? SeizureClusterToday { get; set; }
    public int? SeizureCountLast24Hours { get; set; }
    public string? SeizurePostIctalDescription { get; set; }
    public string? SeizurePossibleTrigger { get; set; }

    // ----------------------------
    // Bleeding-specific fields
   

    public string? BleedingLocation { get; set; }
    public bool? BleedingIsActive { get; set; }
    public string? BleedingAmountEstimate { get; set; }
    public bool? BleedingOnBloodThinnerMedication { get; set; }
    public string? BleedingFirstAidGiven { get; set; }

    // ----------------------------
    // GI-specific fields
    

    public bool? GiVomiting { get; set; }
    public bool? GiDiarrhea { get; set; }
    public bool? GiBloodInVomit { get; set; }
    public bool? GiBloodInStool { get; set; }
    public string? GiLastNormalMealTime { get; set; }
    public bool? GiForeignBodySuspected { get; set; }

    // ----------------------------
    // Cardiac-specific fields
   

    public bool? CardiacKnownHeartDisease { get; set; }
    public string? CardiacRhythmDescription { get; set; }

    // ----------------------------
    // Respiratory-specific fields
  

    public bool? RespLaboredBreathing { get; set; }
    public bool? RespOpenMouthBreathing { get; set; }
    public bool? RespCoughing { get; set; }
    public int? RespRespiratoryRate { get; set; }

    // ----------------------------
    // Trauma-specific fields
    

    public string? TraumaType { get; set; }
    public bool? TraumaLossOfConsciousness { get; set; }
    public string? TraumaVisibleInjuries { get; set; }
    public bool? TraumaLimping { get; set; }
    public bool? TraumaBleedingPresent { get; set; }

    // ----------------------------
    // Accidental ingestion / toxin
  

    public string? IngestionSubstanceName { get; set; }
    public string? IngestionEstimatedAmount { get; set; }
    public DateTime? IngestionApproxTime { get; set; }
    public bool? IngestionVomitedAfter { get; set; }
    public bool? IngestionPoisonControlContacted { get; set; }
    public string? IngestionPoisonControlCaseNumber { get; set; }
    public string? IngestionPoisonControlAdvice { get; set; }

    // ----------------------------
    // Urinary-specific fields
   

    public bool? UrinaryStraining { get; set; }
    public bool? UrinaryFrequentSmallAmounts { get; set; }
    public bool? UrinaryBloodInUrine { get; set; }
    public string? UrinaryLastNormalUrinationTime { get; set; }

    // ----------------------------
    // Pain / Mobility-specific fields
    

    public bool? PainSuddenOnset { get; set; }
    public string? PainLocation { get; set; }
    public bool? PainNonWeightBearing { get; set; }
    public int? PainScoreOutOfTen { get; set; }
    public bool? PainCryingOut { get; set; }

    // ----------------------------
    // Behavior-specific fields
  

    public bool? BehaviorSuddenAggression { get; set; }
    public bool? BehaviorRestlessOrPacing { get; set; }
    public bool? BehaviorDisorientation { get; set; }
    public bool? BehaviorGettingStuckInCorners { get; set; }
    public bool? BehaviorHidingOrWithdrawn { get; set; }

    // ----------------------------
    // Medication-specific fields
    

    public bool? MedMissedDose { get; set; }
    public bool? MedExtraDose { get; set; }
    public string? MedName { get; set; }
    public string? MedEstimatedExtraAmount { get; set; }
    public DateTime? MedEventTime { get; set; }
    public string? MedObservedSideEffects { get; set; }

    // ----------------------------
    // Other / custom
   

    public string? OtherTitle { get; set; }
}
