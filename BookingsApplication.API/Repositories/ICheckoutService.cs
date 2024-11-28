using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookingsApplication.API.DTOs;
using BookingsApplication.API.Models.Domains;

namespace BookingsApplication.API.Repositories
{
    public interface ICheckoutService
    {
        Task<decimal> GetSubTotal(List<AddBookingItemDto> bookingItems);
        Task<Guid?> TicketTypeValidation(List<AddBookingItemDto> bookingItems);
        decimal GetTotalAmount(decimal subTotal, CouponCode? couponModel);
        Task<CouponCode?> ValidateCouponCode(string coupon);
        Task<List<string>> ValidateQuantities(List<AddBookingItemDto> bookingItems);
    }
}