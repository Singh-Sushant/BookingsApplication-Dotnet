using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using BookingsApplication.API.Data;
using BookingsApplication.API.DTOs;
using BookingsApplication.API.Models.Domains;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;

namespace BookingsApplication.API.Repositories
{
    public class SQLBookingsRepository : IBookingsRepository
    {
        private readonly BookingAppDBcontext dBcontext;
        private readonly IMapper mapper;
        public SQLBookingsRepository(IMapper mapper , BookingAppDBcontext dBcontext)
        {
            this.mapper = mapper;
            this.dBcontext = dBcontext;
            
        }


        public async Task<Booking?> createBookingAsync(Booking booking)
        {
            var findEvent = await dBcontext.Events.FirstOrDefaultAsync(x=>x.Id == booking.EventId);

            if(findEvent == null){
                // event not present 
                return null;
            }
            // event is present 
            
            // Compare the prices
            if(!((findEvent.TicketPrice)*(booking.NumberOfTickets)  == booking.TotalPrice)){
                return null;
            }


            await dBcontext.Bookings.AddAsync(booking);
            await dBcontext.SaveChangesAsync();

            return booking;
        }

        public async Task<List<Booking>?> getUserEventsAsync(string email)
        {
            var allBookings = await dBcontext.Bookings.Include("Event").Where(x=>x.Email == email).ToListAsync();
            // var allBookings =  dBcontext.Bookings.Include("Event").AsQueryable();
            
            // check if there are bookings 
            if(allBookings == null || allBookings.Count == 0){
                return null;
            }

            return allBookings;
        }

        
    }
}