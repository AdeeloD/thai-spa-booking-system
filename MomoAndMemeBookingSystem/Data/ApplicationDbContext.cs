using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MomoAndMemeBookingSystem.Models;

namespace MomoAndMemeBookingSystem.Data
{
    public class ApplicationDbContext : IdentityDbContext<IdentityUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<MassageType> MassageTypes { get; set; }
        public DbSet<Masseur> Masseurs { get; set; }
        public DbSet<Booking> Bookings { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<Booking>()
            .Property(b => b.Date)
            .HasColumnType("timestamp with time zone");

            // PostgreSQL Identity adattípus konfiguráció
            builder.Entity<IdentityUser>(entity =>
            {
                entity.Property(e => e.Id).HasColumnType("text");
                entity.Property(e => e.UserName).HasColumnType("text");
                entity.Property(e => e.NormalizedUserName).HasColumnType("text");
                entity.Property(e => e.Email).HasColumnType("text");
                entity.Property(e => e.NormalizedEmail).HasColumnType("text");
                entity.Property(e => e.PasswordHash).HasColumnType("text");
                entity.Property(e => e.SecurityStamp).HasColumnType("text");
                entity.Property(e => e.ConcurrencyStamp).HasColumnType("text");
            });

            builder.Entity<IdentityRole>(entity =>
            {
                entity.Property(e => e.Id).HasColumnType("text");
                entity.Property(e => e.Name).HasColumnType("text");
                entity.Property(e => e.NormalizedName).HasColumnType("text");
                entity.Property(e => e.ConcurrencyStamp).HasColumnType("text");
            });
        }
    }
}
