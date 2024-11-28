using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using BookingsApplication.API.Models.Domains;

namespace BookingsApplication.API.Models.Domains
{
    public class Booking
    {
        [Key]
        public Guid Id { get; set; }
        public string UserId { get; set; } // Foreign Key to ApplicationUser
        public DateTime BookedAt { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal Subtotal { get; set; } // Subtotal before taxes and discounts

        [Column(TypeName = "decimal(18,2)")]
        public decimal Taxes { get; set; } // Taxes applied

        [Column(TypeName = "decimal(18,2)")]
        public decimal TotalAmount { get; set; } // Total amount after taxes and discounts
        public string? CouponCode { get; set; } // Optional coupon code
        public PaymentStatus PaymentStatus { get; set; } // e.g., Pending, Completed, Failed
        public string Paymentcurrency { get; set; } = "INR";
        public Guid TransactionId { get; set; } 
        public string BillingName { get; set; } // Name of the person billed

        [EmailAddress(ErrorMessage = "Invalid email address format.")]
        public string BillingEmail { get; set; } // Email address for booking confirmation

        [RegularExpression(@"^\d{10}$", ErrorMessage = "Phone number must be exactly 10 digits.")]
        public long BillingPhoneNumber { get; set; }
        public BookingStatus BookingStatus { get; set; }

        [Required]
        public Guid EventId {get; set ;}



        // Navigation Property
        [JsonIgnore]
        public virtual User User { get; set; }
        [JsonIgnore]
        public virtual ICollection<BookingItem> BookingItems { get; set; } = new List<BookingItem>(); // Related tickets
        public virtual Event Event {get; set;}
    }

    public enum BookingStatus
    {
        Completed,
        Cancelled
    }

    public enum PaymentStatus
    {
        Pending,
        Failed,
        Successful,
        Refund
    }
}