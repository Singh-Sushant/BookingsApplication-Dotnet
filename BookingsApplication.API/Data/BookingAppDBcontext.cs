using BookingsApplication.API.Models.Domains;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using System;
using System.Collections.Generic;
using System.Text.Json;

namespace BookingsApplication.API.Data
{
    public class BookingAppDBcontext : IdentityDbContext<User>
    {
        public BookingAppDBcontext(DbContextOptions<BookingAppDBcontext> dbContextOptions) : base(dbContextOptions)
        {
        }
       
        public DbSet<Event> Events { get; set; }
        public DbSet<Booking> Bookings { get; set; }
        public DbSet<TicketType> TicketTypes { get; set; }

       
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);


          



            var events = new List<Event>
            {
                new Event
                {
                    Id = Guid.NewGuid(),
                    Category = new[] {"Music", "Concert"},
                    Name = "Bollywood Beats 2024",
                    DateTime = DateTime.Now.AddMonths(1),
                    Venue = "Wankhede Stadium, Mumbai",
                    TicketPrice = 1000, // Base price, you can adjust this
                    Artist = "Arijit Singh",
                    Description = "A mesmerizing night of Bollywood music featuring Arijit Singh.",
                    TotalTickets = 10000,
                    AvailableTickets = 10000,
                    EventImage = new[] {
                        "https://images.unsplash.com/photo-1533229505515-cbc2deef1f53",
                        "https://images.unsplash.com/photo-1494790108377-be9c29b29330"
                    },
                    // UserId = "dbec629a-c989-419a-941f-8c2082c77130"
                },
                // You can add more events here
                new Event
                {
                    Id = Guid.NewGuid(),
                    Category = new[] {"Sports", "Cricket"},
                    Name = "IPL Final 2025",    
                    DateTime = DateTime.Now.AddMonths(8),
                    Venue = "Narendra Modi Stadium, Ahmedabad",
                    TicketPrice = 2000,
                    Artist = "Various Teams",
                    Description = "The grand finale of Indian Premier League 2025.",
                    TotalTickets = 130000,
                    AvailableTickets = 130000,
                    EventImage = new[] {
                        "https://images.unsplash.com/photo-1540747913346-19e32dc3e97e",
                        "https://images.unsplash.com/photo-1531415074968-036ba1b575da"
                    },
                    // UserId = "dbec629a-c989-419a-941f-8c2082c77130" 


                }
            };

            modelBuilder.Entity<Event>().HasData(events);

            // Configure relationships
            


            // var ticketTypes = new List<TicketType>();
            // foreach (var evt in events)
            // {
            //     ticketTypes.AddRange(new[]
            //     {
            //         new TicketType
            //         {
            //             Id = Guid.NewGuid(),
            //             Type = "General",
            //             Price = evt.TicketPrice,
            //             EventId = evt.Id,
            //             NoOfTickets = evt.TotalTickets/3
            //         },
            //         new TicketType
            //         {
            //             Id = Guid.NewGuid(),
            //             Type = "VIP",
            //             Price = evt.TicketPrice * 2,
            //             EventId = evt.Id,
            //             NoOfTickets = evt.TotalTickets/3
            //         },
            //         new TicketType
            //         {
            //             Id = Guid.NewGuid(),
            //             Type = "Premium",
            //             Price = evt.TicketPrice * 3,
            //             EventId = evt.Id,
            //             NoOfTickets = evt.TotalTickets/3
            //         }
            //     });
            // }

            // modelBuilder.Entity<TicketType>().HasData(ticketTypes);
        }
    }
}