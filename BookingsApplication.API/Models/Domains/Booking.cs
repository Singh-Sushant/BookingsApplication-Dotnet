namespace BookingsApplication.API.Models.Domains
{
    public class Booking
    {
        public Guid Id { get; set; }
        public string Username { get; set; }    
        public string Email {  get; set; }

        public string PhoneNumber { get; set; }

        public int NumberOfTickets { get; set; }
        public int TotalPrice { get; set; }

        public Guid EventId { get; set; }


        public Event Event { get; set; }
    }
}
