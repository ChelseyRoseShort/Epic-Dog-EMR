using EpicPetEMR.Api.Models;
using EpicPetEMR.Shared.Models;

namespace EpicPetEMR.Api.Mappings;

public static class OhNoEventMappings
{
    public static OhNoEventDto ToDto(this OhNoEvent e) => new()
    {
        Id = e.Id,
        PetId = e.PetId,
        VetTripId = e.VetTripId,
        BrainTaskId = e.BrainTaskId,
        Type = e.Type,
        OccurredAt = e.OccurredAt,
        Notes = e.Notes,

        EventCollapseOrFainting = e.EventCollapseOrFainting,
        EventDuringOrAfterExercise = e.EventDuringOrAfterExercise,
        EventHeartRateIfKnown = e.EventHeartRateIfKnown,
        EventBlueOrPaleGums = e.EventBlueOrPaleGums,

        SeizureDurationSeconds = e.SeizureDurationSeconds,
        SeizureWasGeneralized = e.SeizureWasGeneralized,
        SeizureLossOfConsciousness = e.SeizureLossOfConsciousness,
        SeizureUrination = e.SeizureUrination,
        SeizureIncontinentBM = e.SeizureIncontinentBM,
        SeizureClusterToday = e.SeizureClusterToday,
        SeizureCountLast24Hours = e.SeizureCountLast24Hours,
        SeizurePostIctalDescription = e.SeizurePostIctalDescription,
        SeizurePossibleTrigger = e.SeizurePossibleTrigger,

        BleedingLocation = e.BleedingLocation,
        BleedingIsActive = e.BleedingIsActive,
        BleedingAmountEstimate = e.BleedingAmountEstimate,
        BleedingOnBloodThinnerMedication = e.BleedingOnBloodThinnerMedication,
        BleedingFirstAidGiven = e.BleedingFirstAidGiven,

        GiVomiting = e.GiVomiting,
        GiDiarrhea = e.GiDiarrhea,
        GiBloodInVomit = e.GiBloodInVomit,
        GiBloodInStool = e.GiBloodInStool,
        GiLastNormalMealTime = e.GiLastNormalMealTime,
        GiForeignBodySuspected = e.GiForeignBodySuspected,

        CardiacKnownHeartDisease = e.CardiacKnownHeartDisease,
        CardiacRhythmDescription = e.CardiacRhythmDescription,

        RespLaboredBreathing = e.RespLaboredBreathing,
        RespOpenMouthBreathing = e.RespOpenMouthBreathing,
        RespCoughing = e.RespCoughing,
        RespRespiratoryRate = e.RespRespiratoryRate,

        TraumaType = e.TraumaType,
        TraumaLossOfConsciousness = e.TraumaLossOfConsciousness,
        TraumaVisibleInjuries = e.TraumaVisibleInjuries,
        TraumaLimping = e.TraumaLimping,
        TraumaBleedingPresent = e.TraumaBleedingPresent,

        IngestionSubstanceName = e.IngestionSubstanceName,
        IngestionEstimatedAmount = e.IngestionEstimatedAmount,
        IngestionApproxTime = e.IngestionApproxTime,
        IngestionVomitedAfter = e.IngestionVomitedAfter,
        IngestionPoisonControlContacted = e.IngestionPoisonControlContacted,
        IngestionPoisonControlCaseNumber = e.IngestionPoisonControlCaseNumber,
        IngestionPoisonControlAdvice = e.IngestionPoisonControlAdvice,

        UrinaryStraining = e.UrinaryStraining,
        UrinaryFrequentSmallAmounts = e.UrinaryFrequentSmallAmounts,
        UrinaryBloodInUrine = e.UrinaryBloodInUrine,
        UrinaryLastNormalUrinationTime = e.UrinaryLastNormalUrinationTime,

        PainSuddenOnset = e.PainSuddenOnset,
        PainLocation = e.PainLocation,
        PainNonWeightBearing = e.PainNonWeightBearing,
        PainScoreOutOfTen = e.PainScoreOutOfTen,
        PainCryingOut = e.PainCryingOut,

        BehaviorSuddenAggression = e.BehaviorSuddenAggression,
        BehaviorRestlessOrPacing = e.BehaviorRestlessOrPacing,
        BehaviorDisorientation = e.BehaviorDisorientation,
        BehaviorGettingStuckInCorners = e.BehaviorGettingStuckInCorners,
        BehaviorHidingOrWithdrawn = e.BehaviorHidingOrWithdrawn,

        MedMissedDose = e.MedMissedDose,
        MedExtraDose = e.MedExtraDose,
        MedName = e.MedName,
        MedEstimatedExtraAmount = e.MedEstimatedExtraAmount,
        MedEventTime = e.MedEventTime,
        MedObservedSideEffects = e.MedObservedSideEffects,

        OtherTitle = e.OtherTitle
    };

    public static OhNoEvent ToEntity(this OhNoEventDto dto) => new()
    {
        Id = dto.Id,   
        PetId = dto.PetId,
        VetTripId = dto.VetTripId,
        BrainTaskId = dto.BrainTaskId,
        Type = dto.Type,
        OccurredAt = dto.OccurredAt,
        Notes = dto.Notes,

        EventCollapseOrFainting = dto.EventCollapseOrFainting,
        EventDuringOrAfterExercise = dto.EventDuringOrAfterExercise,
        EventHeartRateIfKnown = dto.EventHeartRateIfKnown,
        EventBlueOrPaleGums = dto.EventBlueOrPaleGums,

        SeizureDurationSeconds = dto.SeizureDurationSeconds,
        SeizureWasGeneralized = dto.SeizureWasGeneralized,
        SeizureLossOfConsciousness = dto.SeizureLossOfConsciousness,
        SeizureUrination = dto.SeizureUrination,
        SeizureIncontinentBM = dto.SeizureIncontinentBM,
        SeizureClusterToday = dto.SeizureClusterToday,
        SeizureCountLast24Hours = dto.SeizureCountLast24Hours,
        SeizurePostIctalDescription = dto.SeizurePostIctalDescription,
        SeizurePossibleTrigger = dto.SeizurePossibleTrigger,

        BleedingLocation = dto.BleedingLocation,
        BleedingIsActive = dto.BleedingIsActive,
        BleedingAmountEstimate = dto.BleedingAmountEstimate,
        BleedingOnBloodThinnerMedication = dto.BleedingOnBloodThinnerMedication,
        BleedingFirstAidGiven = dto.BleedingFirstAidGiven,

        GiVomiting = dto.GiVomiting,
        GiDiarrhea = dto.GiDiarrhea,
        GiBloodInVomit = dto.GiBloodInVomit,
        GiBloodInStool = dto.GiBloodInStool,
        GiLastNormalMealTime = dto.GiLastNormalMealTime,
        GiForeignBodySuspected = dto.GiForeignBodySuspected,

        CardiacKnownHeartDisease = dto.CardiacKnownHeartDisease,
        CardiacRhythmDescription = dto.CardiacRhythmDescription,

        RespLaboredBreathing = dto.RespLaboredBreathing,
        RespOpenMouthBreathing = dto.RespOpenMouthBreathing,
        RespCoughing = dto.RespCoughing,
        RespRespiratoryRate = dto.RespRespiratoryRate,

        TraumaType = dto.TraumaType,
        TraumaLossOfConsciousness = dto.TraumaLossOfConsciousness,
        TraumaVisibleInjuries = dto.TraumaVisibleInjuries,
        TraumaLimping = dto.TraumaLimping,
        TraumaBleedingPresent = dto.TraumaBleedingPresent,

        IngestionSubstanceName = dto.IngestionSubstanceName,
        IngestionEstimatedAmount = dto.IngestionEstimatedAmount,
        IngestionApproxTime = dto.IngestionApproxTime,
        IngestionVomitedAfter = dto.IngestionVomitedAfter,
        IngestionPoisonControlContacted = dto.IngestionPoisonControlContacted,
        IngestionPoisonControlCaseNumber = dto.IngestionPoisonControlCaseNumber,
        IngestionPoisonControlAdvice = dto.IngestionPoisonControlAdvice,

        UrinaryStraining = dto.UrinaryStraining,
        UrinaryFrequentSmallAmounts = dto.UrinaryFrequentSmallAmounts,
        UrinaryBloodInUrine = dto.UrinaryBloodInUrine,
        UrinaryLastNormalUrinationTime = dto.UrinaryLastNormalUrinationTime,

        PainSuddenOnset = dto.PainSuddenOnset,
        PainLocation = dto.PainLocation,
        PainNonWeightBearing = dto.PainNonWeightBearing,
        PainScoreOutOfTen = dto.PainScoreOutOfTen,
        PainCryingOut = dto.PainCryingOut,

        BehaviorSuddenAggression = dto.BehaviorSuddenAggression,
        BehaviorRestlessOrPacing = dto.BehaviorRestlessOrPacing,
        BehaviorDisorientation = dto.BehaviorDisorientation,
        BehaviorGettingStuckInCorners = dto.BehaviorGettingStuckInCorners,
        BehaviorHidingOrWithdrawn = dto.BehaviorHidingOrWithdrawn,

        MedMissedDose = dto.MedMissedDose,
        MedExtraDose = dto.MedExtraDose,
        MedName = dto.MedName,
        MedEstimatedExtraAmount = dto.MedEstimatedExtraAmount,
        MedEventTime = dto.MedEventTime,
        MedObservedSideEffects = dto.MedObservedSideEffects,

        OtherTitle = dto.OtherTitle
    };


    public static void UpdateFromDto(this OhNoEvent entity, OhNoEventDto dto)
    {
        entity.Type = dto.Type;
        entity.OccurredAt = dto.OccurredAt;
        entity.Notes = dto.Notes;

        entity.EventCollapseOrFainting = dto.EventCollapseOrFainting;
        entity.EventDuringOrAfterExercise = dto.EventDuringOrAfterExercise;
        entity.EventHeartRateIfKnown = dto.EventHeartRateIfKnown;
        entity.EventBlueOrPaleGums = dto.EventBlueOrPaleGums;

        entity.SeizureDurationSeconds = dto.SeizureDurationSeconds;
        entity.SeizureWasGeneralized = dto.SeizureWasGeneralized;
        entity.SeizureLossOfConsciousness = dto.SeizureLossOfConsciousness;
        entity.SeizureUrination = dto.SeizureUrination;
        entity.SeizureIncontinentBM = dto.SeizureIncontinentBM;
        entity.SeizureClusterToday = dto.SeizureClusterToday;
        entity.SeizureCountLast24Hours = dto.SeizureCountLast24Hours;
        entity.SeizurePostIctalDescription = dto.SeizurePostIctalDescription;
        entity.SeizurePossibleTrigger = dto.SeizurePossibleTrigger;

        entity.BleedingLocation = dto.BleedingLocation;
        entity.BleedingIsActive = dto.BleedingIsActive;
        entity.BleedingAmountEstimate = dto.BleedingAmountEstimate;
        entity.BleedingOnBloodThinnerMedication = dto.BleedingOnBloodThinnerMedication;
        entity.BleedingFirstAidGiven = dto.BleedingFirstAidGiven;

        entity.GiVomiting = dto.GiVomiting;
        entity.GiDiarrhea = dto.GiDiarrhea;
        entity.GiBloodInVomit = dto.GiBloodInVomit;
        entity.GiBloodInStool = dto.GiBloodInStool;
        entity.GiLastNormalMealTime = dto.GiLastNormalMealTime;
        entity.GiForeignBodySuspected = dto.GiForeignBodySuspected;

        entity.CardiacKnownHeartDisease = dto.CardiacKnownHeartDisease;
        entity.CardiacRhythmDescription = dto.CardiacRhythmDescription;

        entity.RespLaboredBreathing = dto.RespLaboredBreathing;
        entity.RespOpenMouthBreathing = dto.RespOpenMouthBreathing;
        entity.RespCoughing = dto.RespCoughing;
        entity.RespRespiratoryRate = dto.RespRespiratoryRate;

        entity.TraumaType = dto.TraumaType;
        entity.TraumaLossOfConsciousness = dto.TraumaLossOfConsciousness;
        entity.TraumaVisibleInjuries = dto.TraumaVisibleInjuries;
        entity.TraumaLimping = dto.TraumaLimping;
        entity.TraumaBleedingPresent = dto.TraumaBleedingPresent;

        entity.IngestionSubstanceName = dto.IngestionSubstanceName;
        entity.IngestionEstimatedAmount = dto.IngestionEstimatedAmount;
        entity.IngestionApproxTime = dto.IngestionApproxTime;
        entity.IngestionVomitedAfter = dto.IngestionVomitedAfter;
        entity.IngestionPoisonControlContacted = dto.IngestionPoisonControlContacted;
        entity.IngestionPoisonControlCaseNumber = dto.IngestionPoisonControlCaseNumber;
        entity.IngestionPoisonControlAdvice = dto.IngestionPoisonControlAdvice;

        entity.UrinaryStraining = dto.UrinaryStraining;
        entity.UrinaryFrequentSmallAmounts = dto.UrinaryFrequentSmallAmounts;
        entity.UrinaryBloodInUrine = dto.UrinaryBloodInUrine;
        entity.UrinaryLastNormalUrinationTime = dto.UrinaryLastNormalUrinationTime;

        entity.PainSuddenOnset = dto.PainSuddenOnset;
        entity.PainLocation = dto.PainLocation;
        entity.PainNonWeightBearing = dto.PainNonWeightBearing;
        entity.PainScoreOutOfTen = dto.PainScoreOutOfTen;
        entity.PainCryingOut = dto.PainCryingOut;

        entity.BehaviorSuddenAggression = dto.BehaviorSuddenAggression;
        entity.BehaviorRestlessOrPacing = dto.BehaviorRestlessOrPacing;
        entity.BehaviorDisorientation = dto.BehaviorDisorientation;
        entity.BehaviorGettingStuckInCorners = dto.BehaviorGettingStuckInCorners;
        entity.BehaviorHidingOrWithdrawn = dto.BehaviorHidingOrWithdrawn;

        entity.MedMissedDose = dto.MedMissedDose;
        entity.MedExtraDose = dto.MedExtraDose;
        entity.MedName = dto.MedName;
        entity.MedEstimatedExtraAmount = dto.MedEstimatedExtraAmount;
        entity.MedEventTime = dto.MedEventTime;
        entity.MedObservedSideEffects = dto.MedObservedSideEffects;

        entity.OtherTitle = dto.OtherTitle;
    }
}
