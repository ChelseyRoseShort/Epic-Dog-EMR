namespace EpicPetEMR.Shared.Models;

public class OhNoEventDto
{
    public int Id { get; set; }

    public int PetId { get; set; }

    public int? VetTripId { get; set; }

    public int? BrainTaskId { get; set; }

    public OhNoEventType Type { get; set; }

    public DateTime OccurredAt { get; set; } = DateTime.UtcNow;

    public string? Notes { get; set; }

    // ----------------------------------------------------
    // Generic "Oh NO" flags that could apply to ANY event
   

    /// <summary>Did they collapse or faint at any point?</summary>
    public bool? EventCollapseOrFainting { get; set; }

    /// <summary>Did this happen during or right after exercise/excitement?</summary>
    public bool? EventDuringOrAfterExercise { get; set; }

    /// <summary>Heart rate if known (bpm) – from monitor or manual count.</summary>
    public int? EventHeartRateIfKnown { get; set; }

    /// <summary>Did gums/tongue look blue or very pale?</summary>
    public bool? EventBlueOrPaleGums { get; set; }

    // ----------------------------
    // Seizure-specific fields
    // Type == OhNoEventType.Seizure
  

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
    // Type == OhNoEventType.Bleeding
   

    public string? BleedingLocation { get; set; }
    public bool? BleedingIsActive { get; set; }
    public string? BleedingAmountEstimate { get; set; }
    public bool? BleedingOnBloodThinnerMedication { get; set; }
    public string? BleedingFirstAidGiven { get; set; }

    // ----------------------------
    // GI-specific fields
    // Type == OhNoEventType.GI
 
    public bool? GiVomiting { get; set; }
    public bool? GiDiarrhea { get; set; }
    public bool? GiBloodInVomit { get; set; }
    public bool? GiBloodInStool { get; set; }
    public string? GiLastNormalMealTime { get; set; }
    public bool? GiForeignBodySuspected { get; set; }

    // ----------------------------
    // Cardiac-specific fields
    // Type == OhNoEventType.Cardiac
   

    public bool? CardiacKnownHeartDisease { get; set; }
    public string? CardiacRhythmDescription { get; set; }

    // ----------------------------
    // Respiratory-specific fields
    // Type == OhNoEventType.Respiratory
    // (Blue/pale gums is generic above)
 

    public bool? RespLaboredBreathing { get; set; }
    public bool? RespOpenMouthBreathing { get; set; }
    public bool? RespCoughing { get; set; }
    public int? RespRespiratoryRate { get; set; }

    // ----------------------------
    // Trauma-specific fields
    // Type == OhNoEventType.Trauma
    

    public string? TraumaType { get; set; }
    public bool? TraumaLossOfConsciousness { get; set; }
    public string? TraumaVisibleInjuries { get; set; }
    public bool? TraumaLimping { get; set; }
    public bool? TraumaBleedingPresent { get; set; }

    // ----------------------------
    // Accidental ingestion / toxin
    // Type == OhNoEventType.AccidentalIngestion
  
    public string? IngestionSubstanceName { get; set; }
    public string? IngestionEstimatedAmount { get; set; }
    public DateTime? IngestionApproxTime { get; set; }
    public bool? IngestionVomitedAfter { get; set; }
    public bool? IngestionPoisonControlContacted { get; set; }
    public string? IngestionPoisonControlCaseNumber { get; set; }
    public string? IngestionPoisonControlAdvice { get; set; }

    // ----------------------------
    // Urinary-specific fields
    // Type == OhNoEventType.Urinary
    

    public bool? UrinaryStraining { get; set; }
    public bool? UrinaryFrequentSmallAmounts { get; set; }
    public bool? UrinaryBloodInUrine { get; set; }
    public string? UrinaryLastNormalUrinationTime { get; set; }

    // ----------------------------
    // Pain / Mobility-specific fields
    // Type == OhNoEventType.PainMobility
   

    public bool? PainSuddenOnset { get; set; }
    public string? PainLocation { get; set; }
    public bool? PainNonWeightBearing { get; set; }
    public int? PainScoreOutOfTen { get; set; }
    public bool? PainCryingOut { get; set; }

    // ----------------------------
    // Behavior-specific fields
    // Type == OhNoEventType.Behavior
    

    public bool? BehaviorSuddenAggression { get; set; }
    public bool? BehaviorRestlessOrPacing { get; set; }
    public bool? BehaviorDisorientation { get; set; }
    public bool? BehaviorGettingStuckInCorners { get; set; }
    public bool? BehaviorHidingOrWithdrawn { get; set; }

    // ----------------------------
    // Medication-specific fields
    // Type == OhNoEventType.Medication
   

    public bool? MedMissedDose { get; set; }
    public bool? MedExtraDose { get; set; }
    public string? MedName { get; set; }
    public string? MedEstimatedExtraAmount { get; set; }
    public DateTime? MedEventTime { get; set; }
    public string? MedObservedSideEffects { get; set; }

    // ----------------------------
    // Other / custom
    // Type == OhNoEventType.Other
    

    public string? OtherTitle { get; set; }
}

