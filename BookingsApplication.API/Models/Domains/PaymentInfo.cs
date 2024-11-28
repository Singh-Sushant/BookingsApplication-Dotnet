using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BookingsApplication.API.Models.Domains
{
    public class PaymentInfo
    {
        [Key]
        public Guid Id { get; set; }
        public int BookingId { get; set; } //foriegn key
        public Guid TransactionId { get; set; }
        public string PaymentMethod { get; set; }

        //navigation property
        // public Booking Booking { get; set; }
    }
}