using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Threading.Tasks;
using BookingsApplication.API.Data;
using BookingsApplication.API.Models.Domains;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;

namespace BookingsApplication.API.Repositories
{
    public class SQLCartRepository : ICartRepository
    {
        private readonly BookingAppDBcontext dBcontext;
        public SQLCartRepository(BookingAppDBcontext dBcontext)
        {
            this.dBcontext = dBcontext;

        }

        public async Task<Cart?> addToCartAsync(Cart cartElement)
        {
            

            // Validate that the event exists
            var eventExists = await dBcontext.Events.AnyAsync(e => e.Id == cartElement.EventId);
            if (!eventExists)
            {
                throw new Exception("Event not found");
            }

            // Validate that the ticket type exists
            var ticketTypeExists = await dBcontext.TicketTypes.AnyAsync(t => t.Id == cartElement.TicketTypeId);
            if (!ticketTypeExists)
            {
                throw new Exception("Ticket type not found");
            }

            if(cartElement.NumberOfTickets <= 0){
                return null;
            }
            // Get user
            var user = await dBcontext.Users.FirstOrDefaultAsync(x => x.Email == cartElement.Email);
            if (user != null)
            {
                cartElement.UserId = user.Id;
            }

            try
            {
                await dBcontext.Carts.AddAsync(cartElement);
                await dBcontext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error saving cart: {ex.Message}");
                return null;
            }

            return await dBcontext.Carts.Include(c => c.Event).Include(c=>c.TicketType).FirstOrDefaultAsync(c=>c.Id == cartElement.Id);
        }

        public async Task<Cart?> deleteCartElementByIdAsync(Guid cartElementId)
        {
            var cartElement = await dBcontext.Carts.FirstOrDefaultAsync(e=>e.Id == cartElementId);
            if(cartElement == null){
                return null;
            }

            dBcontext.Remove(cartElement);
            await dBcontext.SaveChangesAsync(); 
            return cartElement;

        }

        public async Task<List<Cart>?> getAllCartAsync()
        {
            return await dBcontext.Carts.ToListAsync();
        }

        public async Task<List<Cart>?> getCartItemsByEmailAsync(string email)
        {
            return await dBcontext.Carts
                .Where(c => c.Email == email)
                .Include(c => c.Event)
                .Include(c => c.TicketType)
                .ToListAsync();
        }

        public async Task<Cart?> UpdateCartAsync(Guid cartElementId, int numberOfTickets, int totalPrice)
        {
            var cartItem = await dBcontext.Carts.FirstOrDefaultAsync(c => c.Id == cartElementId);
            if(cartItem == null){
                
                return null;
            }
            var ticketType = await dBcontext.TicketTypes.FirstOrDefaultAsync(c=>c.Id == cartItem.TicketTypeId);
            if(ticketType == null){
                System.Console.WriteLine("ticket type not found");
                return null;
            }
            if(numberOfTickets > ticketType.AvailableNoOfTickets || numberOfTickets <= 0){
                System.Console.WriteLine("quantity is not correct");
                return null;
            }
            if(numberOfTickets*ticketType.Price != totalPrice){
                System.Console.WriteLine("total price is not correct");
                return null;
            }
            if (cartItem != null)
            {
                cartItem.NumberOfTickets = numberOfTickets;
                cartItem.TotalPrice = totalPrice;
                await dBcontext.SaveChangesAsync();
            }
            return cartItem;
            }
    }
}