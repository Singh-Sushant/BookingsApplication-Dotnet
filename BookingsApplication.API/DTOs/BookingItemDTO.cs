using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookingsApplication.API.DTOs
{
    public class BookingItemDto
    {
        public Guid EventId { get; set;}
        public string EventName { get; set; }
        public Guid TicketTypeId { get; set; }
        public string TicketTypeName { get; set; }
        public int QuantityBooked { get; set; }
    }
}