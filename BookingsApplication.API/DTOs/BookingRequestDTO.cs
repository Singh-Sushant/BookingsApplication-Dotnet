using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookingsApplication.API.DTOs
{
    public class BookingRequestDTO
    {
        public string Username { get; set; }    
        public string Email {  get; set; }

        public string PhoneNumber { get; set; }

        public int NumberOfTickets { get; set; }
        public int TotalPrice { get; set; }

        public Guid EventId { get; set; }
    }
}