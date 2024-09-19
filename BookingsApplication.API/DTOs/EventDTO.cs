namespace BookingsApplication.API.DTOs
{
    public class EventDTO
    {
        public Guid Id { get; set; }
        public string Category { get; set; }
        public string Name { get; set; }
        public DateTime DateTime { get; set; }
        public string Venue { get; set; }
        public int TicketPrice { get; set; }
        public string Artist { get; set; }
        public string Description { get; set; }
    }
}
