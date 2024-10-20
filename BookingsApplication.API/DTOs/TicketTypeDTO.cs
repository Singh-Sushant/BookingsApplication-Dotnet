using System;
using System.ComponentModel.DataAnnotations;

namespace BookingsApplication.API.DTOs
{
    public class TicketTypeDTO
    {
        public Guid Id { get; set; }
        public Guid EventId { get; set; }
        [Required]
        public string Type { get; set; }
        [Required]
        public int Price { get; set; }
        [Required]
        public int? NoOfTickets { get; set; }
        public int? AvailableNoOfTickets { get; set; }
    }
}