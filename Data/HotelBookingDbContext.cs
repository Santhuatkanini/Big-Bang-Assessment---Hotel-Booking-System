using HotelBooking.Auth;
using HotelBooking.Models;
using Microsoft.EntityFrameworkCore;

public class HotelBookingDbContext : DbContext
{
    public HotelBookingDbContext(DbContextOptions<HotelBookingDbContext> options) : base(options)
    {
    }

    public DbSet<Hotel> Hotels { get; set; }
    public DbSet<Room> Rooms { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Configure one-to-many relationship
        modelBuilder.Entity<Room>()
            .HasOne(r => r.Hotel)
            .WithMany(h => h.Rooms)
            .HasForeignKey(r => r.HotelId);
    }
    public static implicit operator ApplicationDbContext(HotelBookingDbContext v)
    {
        throw new NotImplementedException();
    }
}


