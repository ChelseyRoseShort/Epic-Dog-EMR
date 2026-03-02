using EpicPetEMR.Api.Models;
using EpicPetEMR.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace EpicPetEMR.Api.Data;

public class AppDbContext(DbContextOptions<AppDbContext> options)
    : IdentityDbContext<User, IdentityRole<int>, int>(options)
{
    public DbSet<Family> Families => Set<Family>();
    public DbSet<FamilyMembership> FamilyMemberships => Set<FamilyMembership>();

    public DbSet<Pet> Pets => Set<Pet>();
    public DbSet<Medication> Medications { get; set; } = default!;
    public DbSet<Appointment> Appointments => Set<Appointment>();
    public DbSet<Attachment> Attachments => Set<Attachment>();
    public DbSet<MARHistory> MARHistory { get; set; } = default!;
    public DbSet<VetTrip> VetTrips { get; set; } = null!;
    public DbSet<PetDocument> PetDocuments { get; set; } = null!;
    public DbSet<OhNoEvent> OhNoEvents { get; set; } = null!;
    public DbSet<Avatar> PetFindings => Set<Avatar>();
    public DbSet<PetMedicalHistory> PetMedicalHistories => Set<PetMedicalHistory>();

    protected override void OnModelCreating(ModelBuilder b)
    {
        base.OnModelCreating(b);

        // -----------------------------
        // Family <-> User membership join
        // -----------------------------
        b.Entity<FamilyMembership>()
            .HasKey(x => new { x.FamilyId, x.UserId });

        b.Entity<FamilyMembership>()
            .HasOne(x => x.Family)
            .WithMany(f => f.Memberships)
            .HasForeignKey(x => x.FamilyId)
            .OnDelete(DeleteBehavior.Cascade);

        b.Entity<FamilyMembership>()
            .HasOne(x => x.User)
            .WithMany(u => u.FamilyMemberships)
            .HasForeignKey(x => x.UserId)
            .OnDelete(DeleteBehavior.Cascade);

        // Optional: prevent duplicate roleless rows already covered by PK,
        // but you can add additional indexes later if needed.

        // -----------------------------
        // Family 1 : many Pets
        // -----------------------------
        b.Entity<Family>()
            .HasMany(f => f.Pets)
            .WithOne(p => p.Family)
            .HasForeignKey(p => p.FamilyId)
            .OnDelete(DeleteBehavior.Cascade);

        // -----------------------------
        // Pet relations
        // -----------------------------
        b.Entity<Pet>()
            .HasMany(p => p.Appointments)
            .WithOne(a => a.Pet)
            .HasForeignKey(a => a.PetId)
            .OnDelete(DeleteBehavior.Cascade);

        b.Entity<Pet>()
            .HasMany(p => p.Attachments)
            .WithOne(a => a.Pet)
            .HasForeignKey(a => a.PetId)
            .OnDelete(DeleteBehavior.Cascade);

        // -----------------------------
        // SQLite decimal precision
        // -----------------------------
        b.Entity<Pet>()
            .Property(p => p.Weight)
            .HasPrecision(6, 2);
    }
}
