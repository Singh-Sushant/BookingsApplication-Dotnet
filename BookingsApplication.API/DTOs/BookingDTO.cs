using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using BookingsApplication.API.Models.Domains;

namespace BookingsApplication.API.DTOs
{
    public class BookingDTO
    {
        public Guid Id {get; set;}
        public string Username { get; set; }    
        public string Email {  get; set; }

        public string PhoneNumber { get; set; }

        public int NumberOfTickets { get; set; }
        public int TotalPrice { get; set; }

        public Event Event{get ; set;}
    }
}