using HotelBookingSample.Models;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Emit;

public class HotelBookingDbContext : DbContext
{
    public HotelBookingDbContext(DbContextOptions<HotelBookingDbContext> options) : base(options)
    {
    }

    public DbSet<Hotel> Hotels { get; set; }
    public DbSet<Room> Rooms { get; set; }
    public DbSet<Booking> Bookings { get; set; }



    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
    }

}
