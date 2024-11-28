using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;


namespace BookingsApplication.API.DTOs
{
    public  class BookingRequestDTO
    {
        [Required]
        public List<AddBookingItemDto> BookingItems { get; set; } = new List<AddBookingItemDto>();

        [Required]
        public string BillingName { get; set; } = string.Empty;

        [Required]
        [EmailAddress(ErrorMessage = "Invalid email address format.")]
        public string BillingEmail { get; set; } = string.Empty;

        [Required]
        [RegularExpression(@"^\d{10}$", ErrorMessage = "Phone number must be exactly 10 digits.")]
        public long BillingPhoneNumber { get; set; }

        public string? CouponCode { get; set; }

        [Required]
        public decimal TotalAmount { get; set; }


        [Required]
        public Guid EventId { get; set; }

        [Required]
        public string Paymentcurrency {get; set; } = "INR";

        
    }
}