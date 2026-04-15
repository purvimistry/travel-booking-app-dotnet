using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TravelBookingApp.Domain.Entities;

namespace TravelBookingApp.Infrastructure.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Hotel> Hotels { get; set; }
        public DbSet<Booking> Bookings { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Booking -> Hotel
            modelBuilder.Entity<Booking>()
                .HasOne(b => b.Hotel)
                .WithMany(h => h.Bookings)
                .HasForeignKey(b => b.HotelId)
                .OnDelete(DeleteBehavior.Restrict);  
            
            // Booking -> Customer
            modelBuilder.Entity<Booking>()
                .HasOne(b => b.Customer)
                .WithMany(c => c.Bookings)
                .HasForeignKey(b=>b.CustomerId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Hotel>()
                .Property(h => h.PricePerNight)
                .HasPrecision(18,2);

            modelBuilder.Entity<Booking>()
                .Property(b => b.TotalAmount)
                .HasPrecision(18,2);

            modelBuilder.Entity<Hotel>().HasData(
                new Hotel { HotelId = 1, Name = "The Grand Mumbai", City = "Mumbai", Country = "India", PricePerNight = 4500, StarRating = 5 },
                new Hotel { HotelId = 2, Name = "Delhi Palace Inn", City = "Delhi", Country = "India", PricePerNight = 2800, StarRating = 4 },
                new Hotel { HotelId = 3, Name = "Surat Business Hotel", City = "Surat", Country = "India", PricePerNight = 1800, StarRating = 3 }
                );
            modelBuilder.Entity<Customer>().HasData(
               new Customer { CustomerId = 1, FullName = "Rahul Sharma", Email = "rahul@example.com", PhoneNumber = "9876543210" },
               new Customer { CustomerId = 2, FullName = "Priya Patel", Email = "priya@example.com", PhoneNumber = "9876543211" }
           );
        }
    }
}
