using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;
using BookingsApplication.API.Models.Domains;
namespace BookingsApplication.API.Models.Domains
{
    public class Event
    {
        public Guid Id { get; set; }
        public string[] Category { get; set; }
        public string Name { get; set; }    
        public DateTime DateTime { get; set; }
        public string Venue {  get; set; }
        public int TicketPrice {  get; set; }
        public string Artist { get; set; }
        public string Description { get; set; }

        public int TotalTickets {get; set;}
        public int AvailableTickets { get; set; }
       
        public string[] EventImage { get; set; }
        public string? UserId {get; set;}


        public User User {get; set;}
        public ICollection<TicketType> TicketTypes { get; set; } 

    }
}
