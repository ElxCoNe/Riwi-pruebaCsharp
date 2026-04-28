using Microsoft.EntityFrameworkCore;
using PruebaRiwi.Models;

namespace PruebaRiwi.Data;

public class MysqlDbContext : DbContext
{
    public MysqlDbContext(DbContextOptions<MysqlDbContext> options) : base(options)
    {
        
    }
    
    public DbSet<User> Users { get; set; }
    public DbSet<SportPlace> SportPlaces { get; set; }
    public DbSet<Reservation> Reservations { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        // document and email uniques
        builder.Entity<User>()
            .HasIndex(u => u.Document)
            .IsUnique();

        builder.Entity<User>()
            .HasIndex(u => u.Email)
            .IsUnique();

        // name unique
        builder.Entity<SportPlace>()
            .HasIndex(s => s.Name)
            .IsUnique();

        // Convert enums like strings
        builder.Entity<SportPlace>()
            .Property(s => s.Type)
            .HasConversion<string>();

        builder.Entity<Reservation>()
            .Property(r => r.Status)
            .HasConversion<string>();

        // Relation User -> Reservations
        builder.Entity<Reservation>()
            .HasOne(r => r.User)
            .WithMany(u => u.Reservations)
            .HasForeignKey(r => r.UserId);

        // Relation SportPlace -> Reservations
        builder.Entity<Reservation>()
            .HasOne(r => r.SportPlace)
            .WithMany(s => s.Reservations)
            .HasForeignKey(r => r.SportPlaceId);
    }
    
}