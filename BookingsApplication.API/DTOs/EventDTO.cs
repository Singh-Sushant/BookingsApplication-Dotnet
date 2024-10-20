using System;
using System.Collections.Generic;

namespace BookingsApplication.API.DTOs
{
    public class EventDTO
    {
        public Guid Id { get; set; }
        public string[] Category { get; set; }
        public string Name { get; set; }    
        public DateTime DateTime { get; set; }
        public string Venue { get; set; }
        public int TicketPrice { get; set; }
        public string Artist { get; set; }
        public string Description { get; set; }
        public int TotalTickets { get; set; }
        public int AvailableTickets { get; set; }
        public string[] EventImage { get; set; }
        public string? UserId { get; set; }
        public ICollection<TicketTypeDTO> TicketTypes { get; set; }
    }
}