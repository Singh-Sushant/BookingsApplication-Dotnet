using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;
using BookingsApplication.API.Models.Domains;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace BookingsApplication.API.Models.Domains
{
    public class Event
    {
        public Guid Id { get; set; }
        public string[] Category { get; set; }

        [Required]
        [MaxLength(100)]
        public string Name { get; set; }    
        public DateTime DateTime { get; set; }
        public string Venue {  get; set; }
        public int TicketPrice {  get; set; }
        public string Artist { get; set; }
        [MaxLength(2000)]
        public string Description { get; set; }

        public int TotalTickets {get; set;}
        public int AvailableTickets { get; set; }
       
        public string[] EventImage { get; set; }
        public string? UserId {get; set;}

         [NotMapped]
        public decimal MinTicketPrice
        {
            get
            {
                return TicketTypes.Min(ticket => ticket.AvailableNoOfTickets > 0 ? ticket.Price : decimal.MaxValue);
            }
        }

        public User User {get; set;}
        public ICollection<TicketType> TicketTypes { get; set; } 

    }
}
