using Microsoft.EntityFrameworkCore;
using EpicPetEMR.Api.Models;

namespace EpicPetEMR.Api.Data;

public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
{
    public DbSet<Family> Families => Set<Family>();
    public DbSet<User> Users => Set<User>();
    public DbSet<Pet> Pets => Set<Pet>();
    public DbSet<Medication> Medications { get; set; } = default!;
    public DbSet<Appointment> Appointments => Set<Appointment>();
    public DbSet<Attachment> Attachments => Set<Attachment>();

    public DbSet<MARHistory> MARHistory { get; set; }



    protected override void OnModelCreating(ModelBuilder b)
    {
        base.OnModelCreating(b);

        // Family relations
        b.Entity<Family>()
            .HasMany(f => f.Users).WithOne(u => u.Family)
            .HasForeignKey(u => u.FamilyId).OnDelete(DeleteBehavior.Restrict);

        b.Entity<Family>()
            .HasMany(f => f.Pets).WithOne(p => p.Family)
            .HasForeignKey(p => p.FamilyId).OnDelete(DeleteBehavior.Cascade);

        // Pet relations
        b.Entity<Pet>()
            .HasMany(p => p.Appointments).WithOne(a => a.Pet)
            .HasForeignKey(a => a.PetId).OnDelete(DeleteBehavior.Cascade);

        b.Entity<Pet>()
            .HasMany(p => p.Attachments).WithOne(a => a.Pet)
            .HasForeignKey(a => a.PetId).OnDelete(DeleteBehavior.Cascade);

        // Simple precision for decimals (SQLite)
        b.Entity<Pet>().Property(p => p.Weight).HasPrecision(6, 2);
    }


}
