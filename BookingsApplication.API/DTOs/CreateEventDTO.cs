using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BookingsApplication.API.DTOs
{
    public class CreateEventDTO
    {
        [Required]
        public Guid id {get; set;}
        [Required]
        public string Name { get; set; }
        [Required]
        public string[] Category { get; set; }

        [Required]
        public DateTime DateTime { get; set; }

        [Required]
        public string Venue { get; set; }

        public string Artist { get; set; }

        public string Description { get; set; }

        [Required]
        public int TotalTickets { get; set; }

        [Required]
        public List<TicketTypeDTO> TicketTypes { get; set; }

        public List<string> EventImage { get; set; }
    }
}