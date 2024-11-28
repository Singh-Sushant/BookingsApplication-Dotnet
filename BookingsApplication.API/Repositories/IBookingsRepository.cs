using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using BookingsApplication.API.Models.Domains;

namespace BookingsApplication.API.Repositories
{
    public interface IBookingsRepository
    {
        Task<List<Booking>> GetAllAsync(string appUserId);
        Task<Booking?> GetByIdAsync(Guid id);
        Task<Booking?> AddBookingAsync(Booking bookingModel, string userId);

        // Task<List<Booking>?> DeleteAllAsync(int eventId);

        Task<string?> DeleteByIdAsync(Guid bookingId, string userEmail);
    }
}