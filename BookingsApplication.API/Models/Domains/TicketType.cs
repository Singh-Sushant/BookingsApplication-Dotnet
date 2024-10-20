using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BookingsApplication.API.Models.Domains
{
    public class TicketType
    {
        public Guid Id { get; set; } 
        public Guid EventId { get; set; }
        public string Type { get; set; }
        public int Price { get; set; }
        public int NoOfTickets { get; set; }
        public int? AvailableNoOfTickets { get; set; }

        public Event Event { get; set; }
    }
}