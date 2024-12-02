using Microsoft.EntityFrameworkCore;
using TicketStoreAPI.Models;

namespace TicketStoreAPI.Data
{
    public class TicketStoreAPIContext : DbContext
    {
        public TicketStoreAPIContext(DbContextOptions<TicketStoreAPIContext> options) : base(options)
        {
        }

        public DbSet<Movie> Movies { get; set; }
        public DbSet<Theater> Theaters { get; set; }
        public DbSet<Schedule> Schedules { get; set; }
        public DbSet<Seat> Seats { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Booking> Bookings { get; set; }
        public DbSet<BookingSeat> BookingSeats { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Movie>().ToTable("movies");
            modelBuilder.Entity<Theater>().ToTable("theaters");
            modelBuilder.Entity<Schedule>().ToTable("schedules");
            modelBuilder.Entity<Seat>().ToTable("seats");
            modelBuilder.Entity<User>().ToTable("users");
            modelBuilder.Entity<Booking>().ToTable("bookings");
            modelBuilder.Entity<BookingSeat>().ToTable("booking_seats");

            modelBuilder.Entity<Movie>()
                .HasKey(m => m.MoviesId);

            modelBuilder.Entity<Theater>()
                .HasKey(t => t.TheatersId);

        }
    }
}
