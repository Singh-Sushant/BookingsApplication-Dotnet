using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

using BookingsApplication.API.Models.Domains;

namespace BookingsApplication.API.DTOs
{
    public class BookingDto
    {
        public Guid Id { get; set; }
        public string BillingName { get; set; } = string.Empty;
        public string BillingEmail { get; set; } = string.Empty;
        public long BillingPhoneNumber { get; set; }
        public DateTime BookedAt { get; set; }
        public decimal TotalAmount { get; set; } // Total amount after taxes and discounts
        public PaymentStatus PaymentStatus { get; set; } // e.g., Pending, Completed, Failed
        public string Paymentcurrency { get; set; } = "INR";
        public decimal Subtotal { get; set; } // Subtotal before taxes and discounts
        public decimal Taxes { get; set; } // Taxes applied
        public Guid TransactionId { get; set; } 
        public BookingStatus BookingStatus { get; set; }

        public Guid EventId {get; set; }

        public List<BookingItemDto> BookedItems { get; set; } = new List<BookingItemDto>();
    }
}