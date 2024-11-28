using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Identity;

namespace BookingsApplication.API.Models.Domains
{
    public class User : IdentityUser
    {
        public string PreferredLanguage { get; set; }
        public string PreferredCurrency { get; set; }

        public string ProfilePictureUrl { get; set; } = string.Empty;

        [JsonIgnore]
        public virtual ICollection<Booking> Bookings {get; set;} = new List<Booking>();
    }
}