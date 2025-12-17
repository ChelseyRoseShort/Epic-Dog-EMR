using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EpicPetEMR.Migrations
{
    /// <inheritdoc />
    public partial class AddOhNoEvents : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "OhNoEvents",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    PetId = table.Column<int>(type: "INTEGER", nullable: false),
                    VetTripId = table.Column<int>(type: "INTEGER", nullable: true),
                    BrainTaskId = table.Column<int>(type: "INTEGER", nullable: true),
                    Type = table.Column<int>(type: "INTEGER", nullable: false),
                    OccurredAt = table.Column<DateTime>(type: "TEXT", nullable: false),
                    Notes = table.Column<string>(type: "TEXT", nullable: true),
                    EventCollapseOrFainting = table.Column<bool>(type: "INTEGER", nullable: true),
                    EventDuringOrAfterExercise = table.Column<bool>(type: "INTEGER", nullable: true),
                    EventHeartRateIfKnown = table.Column<int>(type: "INTEGER", nullable: true),
                    EventBlueOrPaleGums = table.Column<bool>(type: "INTEGER", nullable: true),
                    SeizureDurationSeconds = table.Column<int>(type: "INTEGER", nullable: true),
                    SeizureWasGeneralized = table.Column<bool>(type: "INTEGER", nullable: true),
                    SeizureLossOfConsciousness = table.Column<bool>(type: "INTEGER", nullable: true),
                    SeizureUrination = table.Column<bool>(type: "INTEGER", nullable: true),
                    SeizureIncontinentBM = table.Column<bool>(type: "INTEGER", nullable: true),
                    SeizureClusterToday = table.Column<bool>(type: "INTEGER", nullable: true),
                    SeizureCountLast24Hours = table.Column<int>(type: "INTEGER", nullable: true),
                    SeizurePostIctalDescription = table.Column<string>(type: "TEXT", nullable: true),
                    SeizurePossibleTrigger = table.Column<string>(type: "TEXT", nullable: true),
                    BleedingLocation = table.Column<string>(type: "TEXT", nullable: true),
                    BleedingIsActive = table.Column<bool>(type: "INTEGER", nullable: true),
                    BleedingAmountEstimate = table.Column<string>(type: "TEXT", nullable: true),
                    BleedingOnBloodThinnerMedication = table.Column<bool>(type: "INTEGER", nullable: true),
                    BleedingFirstAidGiven = table.Column<string>(type: "TEXT", nullable: true),
                    GiVomiting = table.Column<bool>(type: "INTEGER", nullable: true),
                    GiDiarrhea = table.Column<bool>(type: "INTEGER", nullable: true),
                    GiBloodInVomit = table.Column<bool>(type: "INTEGER", nullable: true),
                    GiBloodInStool = table.Column<bool>(type: "INTEGER", nullable: true),
                    GiLastNormalMealTime = table.Column<string>(type: "TEXT", nullable: true),
                    GiForeignBodySuspected = table.Column<bool>(type: "INTEGER", nullable: true),
                    CardiacKnownHeartDisease = table.Column<bool>(type: "INTEGER", nullable: true),
                    CardiacRhythmDescription = table.Column<string>(type: "TEXT", nullable: true),
                    RespLaboredBreathing = table.Column<bool>(type: "INTEGER", nullable: true),
                    RespOpenMouthBreathing = table.Column<bool>(type: "INTEGER", nullable: true),
                    RespCoughing = table.Column<bool>(type: "INTEGER", nullable: true),
                    RespRespiratoryRate = table.Column<int>(type: "INTEGER", nullable: true),
                    TraumaType = table.Column<string>(type: "TEXT", nullable: true),
                    TraumaLossOfConsciousness = table.Column<bool>(type: "INTEGER", nullable: true),
                    TraumaVisibleInjuries = table.Column<string>(type: "TEXT", nullable: true),
                    TraumaLimping = table.Column<bool>(type: "INTEGER", nullable: true),
                    TraumaBleedingPresent = table.Column<bool>(type: "INTEGER", nullable: true),
                    IngestionSubstanceName = table.Column<string>(type: "TEXT", nullable: true),
                    IngestionEstimatedAmount = table.Column<string>(type: "TEXT", nullable: true),
                    IngestionApproxTime = table.Column<DateTime>(type: "TEXT", nullable: true),
                    IngestionVomitedAfter = table.Column<bool>(type: "INTEGER", nullable: true),
                    IngestionPoisonControlContacted = table.Column<bool>(type: "INTEGER", nullable: true),
                    IngestionPoisonControlCaseNumber = table.Column<string>(type: "TEXT", nullable: true),
                    IngestionPoisonControlAdvice = table.Column<string>(type: "TEXT", nullable: true),
                    UrinaryStraining = table.Column<bool>(type: "INTEGER", nullable: true),
                    UrinaryFrequentSmallAmounts = table.Column<bool>(type: "INTEGER", nullable: true),
                    UrinaryBloodInUrine = table.Column<bool>(type: "INTEGER", nullable: true),
                    UrinaryLastNormalUrinationTime = table.Column<string>(type: "TEXT", nullable: true),
                    PainSuddenOnset = table.Column<bool>(type: "INTEGER", nullable: true),
                    PainLocation = table.Column<string>(type: "TEXT", nullable: true),
                    PainNonWeightBearing = table.Column<bool>(type: "INTEGER", nullable: true),
                    PainScoreOutOfTen = table.Column<int>(type: "INTEGER", nullable: true),
                    PainCryingOut = table.Column<bool>(type: "INTEGER", nullable: true),
                    BehaviorSuddenAggression = table.Column<bool>(type: "INTEGER", nullable: true),
                    BehaviorRestlessOrPacing = table.Column<bool>(type: "INTEGER", nullable: true),
                    BehaviorDisorientation = table.Column<bool>(type: "INTEGER", nullable: true),
                    BehaviorGettingStuckInCorners = table.Column<bool>(type: "INTEGER", nullable: true),
                    BehaviorHidingOrWithdrawn = table.Column<bool>(type: "INTEGER", nullable: true),
                    MedMissedDose = table.Column<bool>(type: "INTEGER", nullable: true),
                    MedExtraDose = table.Column<bool>(type: "INTEGER", nullable: true),
                    MedName = table.Column<string>(type: "TEXT", nullable: true),
                    MedEstimatedExtraAmount = table.Column<string>(type: "TEXT", nullable: true),
                    MedEventTime = table.Column<DateTime>(type: "TEXT", nullable: true),
                    MedObservedSideEffects = table.Column<string>(type: "TEXT", nullable: true),
                    OtherTitle = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OhNoEvents", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OhNoEvents_Pets_PetId",
                        column: x => x.PetId,
                        principalTable: "Pets",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_OhNoEvents_VetTrips_VetTripId",
                        column: x => x.VetTripId,
                        principalTable: "VetTrips",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_OhNoEvents_PetId",
                table: "OhNoEvents",
                column: "PetId");

            migrationBuilder.CreateIndex(
                name: "IX_OhNoEvents_VetTripId",
                table: "OhNoEvents",
                column: "VetTripId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "OhNoEvents");
        }
    }
}
