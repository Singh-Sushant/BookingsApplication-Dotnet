using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using BookingsApplication.API.DTOs;
using BookingsApplication.API.Models.Domains;
using BookingsApplication.API.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace BookingsApplication.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BookingsController : ControllerBase
    {
        private readonly IMapper mapper;
        private readonly IBookingsRepository bookingsRepository;

        public BookingsController(IMapper mapper , IBookingsRepository bookingsRepository)
        {
            this.mapper = mapper;
            this.bookingsRepository = bookingsRepository;
        }

        [HttpPost]
        public async Task<IActionResult> createBooking([FromBody] BookingRequestDTO bookingRequestDTO )
        {
            var bookingDomainModel = mapper.Map<Booking>(bookingRequestDTO);
            var bookingResult = await bookingsRepository.createBookingAsync(bookingDomainModel); 

            if(bookingResult == null){
                // booking did not happen 
                return BadRequest("Could not perform booking for this event");

            }
            // booking done
            return Ok(mapper.Map<BookingDTO>(bookingResult));
        }   

        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> getBookingsOfEvent([FromRoute] Guid id){
            var allBookingsOfEvent = await bookingsRepository.getBookingsofEventAsync(id);
            if(allBookingsOfEvent == null){
                return BadRequest("No bookings for this event available");
            }
            return Ok(mapper.Map<List<BookingDTO>>(allBookingsOfEvent));
        }   
    }
}