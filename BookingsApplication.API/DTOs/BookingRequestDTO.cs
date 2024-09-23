using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BookingsApplication.API.DTOs
{
    public class BookingRequestDTO
    {
        [Required]
        public string Username { get; set; }    
        [Required]
        [DataType(DataType.EmailAddress)]
        public string Email {  get; set; }

        [MaxLength(10 , ErrorMessage ="Max 10 digits required")]
        [MinLength(10 , ErrorMessage = "Minimum 10 digits required")]
        public string PhoneNumber { get; set; }

        [Required]
        [Range(1,100)]
        public int NumberOfTickets { get; set; }

        [Required]
        public int TotalPrice { get; set; }


        public Guid EventId { get; set; }
    }
}