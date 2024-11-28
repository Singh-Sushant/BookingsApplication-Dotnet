using AutoMapper;
using BookingsApplication.API.DTOs;
using BookingsApplication.API.Models.Domains;
using Microsoft.Data.SqlClient;

namespace BookingsApplication.API.Mappings
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<Event, EventRequestDTO>().ReverseMap();
            CreateMap<Event, EventDTO>().ReverseMap();
            
            CreateMap<Booking, BookingRequestDTO>().ReverseMap();
            CreateMap<Booking, BookingDto>()
                .ForMember(dest => dest.BookedItems, opt => opt.MapFrom(src => src.BookingItems));
            
            CreateMap<TicketType, TicketTypeDTO>().ReverseMap();
            CreateMap<CreateEventDTO, Event>().ReverseMap();
            
            CreateMap<Cart, CartDTO>()
                .ForMember(dest => dest.Event, opt => opt.MapFrom(src => src.Event))
                .ForMember(dest => dest.TicketType, opt => opt.MapFrom(src => src.TicketType))
                .ForMember(dest => dest.User, opt => opt.MapFrom(src => src.User))
                .ForMember(dest=> dest.NumberOfTickets, opt => opt.MapFrom(src=> src.NumberOfTickets) );
            
            CreateMap<AddBookingItemDto, BookingItem>();
            CreateMap<BookingRequestDTO, Booking>()
            .ForMember(dest => dest.BookingItems, opt => opt.MapFrom(src => src.BookingItems));
            
            // Mapping for BookingItem to BookingItemDto (with corrected type for TicketTypeId)
            CreateMap<BookingItem, BookingItemDto>();

            CreateMap<Event, EventDTO>()
            .ForMember(dest => dest.TicketTypes, opt => opt.MapFrom(src => src.TicketTypes));


        }
    }
}
