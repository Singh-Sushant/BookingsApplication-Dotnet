using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace BookingsApplication.API.Models.Domains
{
    public class User : IdentityUser
    {
        public string PreferredLanguage { get; set; }
        public string PreferredCurrency { get; set; }

        public string ProfilePictureUrl { get; set; } = string.Empty;
        public Booking[] bookings { get; set; }
    }
}