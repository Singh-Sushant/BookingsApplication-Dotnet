using BookingsApplication.API.Models.Domains;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;

namespace BookingsApplication.API.Data
{
    public class BookingAppDBcontext : IdentityDbContext<User>
    {
        public BookingAppDBcontext(DbContextOptions<BookingAppDBcontext> dbContextOptions) : base(dbContextOptions)
        {
        }
        

        public DbSet<Event> Events { get; set; }
        public DbSet<Booking> Bookings { get; set; }



        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            var concertData = new List<Event>
            {
                new Event
                {
                    Id = Guid.Parse("d290f1ee-6c54-4b01-90e6-d701748f0851"),
                    Category = "Concert",
                    Name = "Rock The Night",
                    DateTime = new DateTime(2024, 10, 15, 19, 30, 0),
                    Venue = "Madison Square Garden, New York",
                    TicketPrice = 99,
                    Artist = "The Rolling Stones",
                    Description = "An electrifying night with the biggest rock bands!"
                },
                new Event
                {
                    Id = Guid.Parse("3fa85f64-5717-4562-b3fc-2c963f66afa6"),
                    Category = "Comedy Show",
                    Name = "Laugh Riot",
                    DateTime = new DateTime(2024, 11, 5, 18, 0, 0),
                    Venue = "The Comedy Store, Los Angeles",
                    TicketPrice = 49,
                    Artist = "Dave Chappelle",
                    Description = "A hilarious stand-up comedy night featuring top comedians."
                },
                new Event
                {
                    Id = Guid.Parse("a34b1f0a-7c5e-4df5-a312-fc90bbd6356d"),
                    Category = "Theater",
                    Name = "Shakespeare's Hamlet",
                    DateTime = new DateTime(2024, 12, 1, 20, 0, 0),
                    Venue = "The Globe Theatre, London",
                    TicketPrice = 120,
                    Artist = "London Shakespeare Company",
                    Description = "A modern rendition of the classic play, Hamlet."
                },
                new Event
                {
                    Id = Guid.Parse("d81b2f5e-9c1e-4817-b6c5-bc3b146b2177"),
                    Category = "Concert",
                    Name = "Jazz Vibes",
                    DateTime = new DateTime(2024, 10, 20, 21, 0, 0),
                    Venue = "Blue Note, Tokyo",
                    TicketPrice = 79,
                    Artist = "Herbie Hancock",
                    Description = "A smooth jazz evening with international jazz artists."
                },
                new Event
                {
                    Id = Guid.Parse("e4b1d89f-8272-4a2b-b8b8-2276f497912b"),
                    Category = "Magic Show",
                    Name = "Illusions Unveiled",
                    DateTime = new DateTime(2024, 10, 31, 19, 0, 0),
                    Venue = "Caesars Palace, Las Vegas",
                    TicketPrice = 150,
                    Artist = "David Copperfield",
                    Description = "A night of mind-bending magic and illusions."
                }
            };

            modelBuilder.Entity<Event>().HasData(concertData);
        }
    }
}
