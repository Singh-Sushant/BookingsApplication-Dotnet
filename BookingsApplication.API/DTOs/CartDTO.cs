using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookingsApplication.API.Models.Domains;

namespace BookingsApplication.API.DTOs
{
    public class CartDTO
    {
        public Guid Id {get; set;}
        public string Email { get; set; }
        public User User {get; set ;}
        public Guid EventId { get; set; }
        public Event Event {get; set;}

        public Guid TicketTypeId {get ; set;}
        public TicketType TicketType {get ; set;}

        public int NumberOfTickets { get; set; }
        public int TotalPrice { get; set; }


    }
}