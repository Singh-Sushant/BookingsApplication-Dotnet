using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace BookingsApplication.API.Models.Domains
{
    public class Cart
    {
        public Guid Id { get; set; }
        
        public string Email { get; set; }

        // Define UserId as a Guid to represent the foreign key.
        public string UserId { get; set; }
        
        // Use User as the navigation property for the relationship.
        public User User { get; set; }

        public Guid EventId { get; set; }
        public Event Event { get; set; }

        public Guid TicketTypeId { get; set; }
        public TicketType TicketType { get; set; }
        
            
        public int NumberOfTickets { get; set; }
        public int TotalPrice { get; set; }
    }
}
