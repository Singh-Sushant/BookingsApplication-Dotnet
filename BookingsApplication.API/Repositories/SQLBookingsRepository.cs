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
        private readonly BookingAppDBcontext _context;
        public SQLBookingsRepository(BookingAppDBcontext bookingAppDBcontext)
        {
            this._context = bookingAppDBcontext;
            
        }
        
         public async Task<Booking?> AddBookingAsync(Booking bookingModel, string userId)
        {
            
                var addedBooking = await _context.Bookings.AddAsync(bookingModel);

                //Retrieve ticket types in bulk
                var ticketTypeIds = bookingModel.BookingItems.Select(item => item.TicketTypeId).ToList();
                var ticketTypes = await _context.TicketTypes
                    .Where(t => ticketTypeIds.Contains(t.Id))
                    .ToListAsync();


                // Update booked tickets
                foreach (var item in bookingModel.BookingItems)
                {
                    var ticketTypeModel = ticketTypes.FirstOrDefault(t => t.Id == item.TicketTypeId);
                    if (ticketTypeModel != null)
                    {
                        ticketTypeModel.AvailableNoOfTickets -= item.Quantity;
                    }
                }

                //empty cart or remove cart
                var cartModel = await _context.Carts.FirstOrDefaultAsync(c => c.UserId == userId);
                if(cartModel != null)
                {
                    _context.Carts.Remove(cartModel);
                }

                await _context.SaveChangesAsync();

                return addedBooking.Entity;
           
        }

        public async Task<string?> DeleteByIdAsync(Guid bookingId, string userEmail)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == userEmail);
            if (user == null)
            {
                return null;
            }
            
            var userId = user.Id;
            var booking = await _context.Bookings.FirstOrDefaultAsync(e => e.Id == bookingId && e.UserId == userId);

            if (booking == null)
            {
                return null;
            }

            _context.Bookings.Remove(booking);
            foreach (var item in booking.BookingItems)
                {

                    var ticketTypeModel = _context.TicketTypes.FirstOrDefault(t => t.Id == item.TicketTypeId);
                    if (ticketTypeModel != null)
                    {
                        ticketTypeModel.AvailableNoOfTickets += item.Quantity;
                    }
                }
            await _context.SaveChangesAsync();

            return "Booking deleted successfully";
        }

        public async Task<List<Booking>> GetAllAsync(string appUserId)
        {
            var bookings = await _context.Bookings
                                        .Include(b => b.BookingItems)
                                        .Where(b => b.UserId == appUserId)
                                        .ToListAsync();
            return bookings;
        }

        public async Task<Booking?> GetByIdAsync(Guid id)
        {
            try{
                var bookingModel = await _context.Bookings.FirstOrDefaultAsync(x => x.Id == id);
                if(bookingModel==null) return null;
                return bookingModel;
            }
            catch(Exception ex){
                return null;
            }
        }
    }
}