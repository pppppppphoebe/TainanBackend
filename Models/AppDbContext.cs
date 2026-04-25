using Microsoft.EntityFrameworkCore;

namespace TainanBackend.Models;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    public DbSet<Restaurant> Restaurants { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Restaurant>(entity =>
        {
            entity.ToTable("restaurants");
            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Name).HasColumnName("name");
            entity.Property(e => e.Address).HasColumnName("address");
            entity.Property(e => e.OpeningHours).HasColumnName("opening_hours");
            entity.Property(e => e.Location).HasColumnName("location");
            entity.Property(e => e.GoogleMapsUrl).HasColumnName("google_maps_url");
            entity.Property(e => e.Rating).HasColumnName("rating");
            entity.Property(e => e.ReviewCount).HasColumnName("review_count");
        });
    }
}