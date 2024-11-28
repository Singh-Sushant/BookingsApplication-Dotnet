
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
        public DbSet<BookingItem> BookingItems { get; set; }
        public DbSet<TicketType> TicketTypes { get; set; }
        public DbSet<Cart> Carts {get; set;}
        public DbSet<CouponCode> CouponCodes { get; set; }




        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);


           

                    modelBuilder.Entity<Cart>()
                .HasOne(c => c.TicketType)
                .WithMany()
                .HasForeignKey(c => c.TicketTypeId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Cart>()
                .HasOne(c => c.Event)
                .WithMany()
                .HasForeignKey(c => c.EventId)
                .OnDelete(DeleteBehavior.Restrict);


            modelBuilder.Entity<CouponCode>().HasData(
                new CouponCode
                {
                    Id = Guid.NewGuid(),
                    CouponName = "DISCOUNT10",
                    CouponType = "Percentage",
                    Discount = 10,
                    MiniBillAmountRequired = 1000,
                    IsActive = true
                },
                new CouponCode
                {
                    Id = Guid.NewGuid(),
                    CouponName = "FLAT500",
                    CouponType = "Flat",
                    Discount = 500,
                    MiniBillAmountRequired = 2000,
                    IsActive = true
                },
                new CouponCode
                {
                    Id = Guid.NewGuid(),
                    CouponName = "SUMMER20",
                    CouponType = "Percentage",
                    Discount = 20,
                    MiniBillAmountRequired = 3000,
                    IsActive = true
                }
            );


        }
    }
}