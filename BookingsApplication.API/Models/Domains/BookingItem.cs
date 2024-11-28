using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using BookingsApplication.API.Models.Domains;

namespace BookingsApplication.API.Models.Domains
{
    public class BookingItem
    {
    
        public Guid Id { get; set; }
        public Guid BookingId { get; set; } // foeriegn key
        public Guid TicketTypeId { get; set; } // foriegn key
        public int Quantity { get; set; }



        [JsonIgnore]
        //navigation Property
        public TicketType TicketType { get; set; }

        [JsonIgnore]
        public Booking Booking { get; set; }
    }
}