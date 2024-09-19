using AutoMapper;
using BookingsApplication.API.DTOs;
using BookingsApplication.API.Models.Domains;

namespace BookingsApplication.API.Mappings
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<Event, EventRequestDTO>().ReverseMap();
            CreateMap<Event,EventDTO>().ReverseMap();
            
            CreateMap<Booking , BookingRequestDTO>().ReverseMap();
            CreateMap<Booking , BookingDTO>().ReverseMap();
        }
    }
}
