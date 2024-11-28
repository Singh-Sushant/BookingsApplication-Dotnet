using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookingsApplication.API.Models.Domains;

namespace BookingsApplication.API.DTOs
{
    public class AddToCartDTO
    {
        public string Email { get; set; }
        

        public Guid EventId { get; set; }

        public Guid TicketTypeId { get; set; }
        public int NumberOfTickets {get; set;}
        
        public int TotalPrice {get; set;}
    }
}