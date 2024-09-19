using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;

namespace BookingsApplication.API.Models.Domains
{
    public class Event
    {
        public Guid Id { get; set; }
        public string Category { get; set; }
        public string Name { get; set; }
        public DateTime DateTime { get; set; }
        public string Venue {  get; set; }
        public int TicketPrice {  get; set; }
        public string Artist { get; set; }
        public string Description { get; set; }
    }
}
