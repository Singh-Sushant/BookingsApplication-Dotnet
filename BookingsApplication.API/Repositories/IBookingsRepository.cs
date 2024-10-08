using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookingsApplication.API.Models.Domains;

namespace BookingsApplication.API.Repositories
{
    public interface IBookingsRepository
    {

        Task<Booking?> createBookingAsync(Booking booking);

        Task<List<Booking>?> getUserEventsAsync(string email);

        Task<Booking?> cancelBookingAsync(Guid id);
        
    }
}