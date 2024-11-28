using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookingsApplication.API.DTOs
{
    public class UpdateCartDTO
{
    public Guid CartElementId { get; set; }
    public int NumberOfTickets { get; set; }
    public int TotalPrice { get; set; }
}

}