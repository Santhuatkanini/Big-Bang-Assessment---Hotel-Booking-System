using HotelBookingSample.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace HotelBookingSample.Data
{
    public class UserDbContext : IdentityDbContext<User, Role, int>
    {
        public UserDbContext(DbContextOptions<UserDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<User>()
                .Property(u => u.FirstName)
                .IsRequired();

            builder.Entity<User>()
                .Property(u => u.LastName)
                .IsRequired();

            builder.Entity<User>()
                .Property(u => u.DateOfBirth)
                .IsRequired();

            builder.Entity<User>()
                .Property(u => u.Address)
                .IsRequired();

            builder.Entity<User>()
                .Property(u => u.City)
                .IsRequired();

            builder.Entity<User>()
                .Property(u => u.Country)
                .IsRequired();

            builder.Entity<User>()
                .Property(u => u.PostalCode)
                .IsRequired();

            builder.Entity<User>()
                .Property(u => u.MobileNo)
                .IsRequired();

            
        }
    }
}
