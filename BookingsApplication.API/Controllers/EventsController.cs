using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using BookingsApplication.API.DTOs;
using BookingsApplication.API.Models.Domains;
using BookingsApplication.API.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BookingsApplication.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EventsController : ControllerBase
    {
        private readonly IEventRepository eventRepository;
        private readonly IMapper mapper;

        public EventsController(IEventRepository eventRepository , IMapper mapper)
        {
            this.eventRepository = eventRepository;
            this.mapper = mapper;
        }



            // /api/events
        [HttpGet]
        //[Authorize]
        public async Task<IActionResult> getAllEvents()
        {
            var allEvents = await eventRepository.getAllEventsAsync();
            return Ok(mapper.Map<List<EventDTO>>(allEvents));
        }

        [HttpGet]
        [Route("{id}")]   
        public async Task<IActionResult> getEventById([FromRoute] Guid id){
            var presentEvent = await eventRepository.getEventByIdAsync(id);
            if(presentEvent != null){
                return Ok(mapper.Map<EventDTO>(presentEvent));
            }
            return BadRequest();
        }


        [HttpPost]
        public async Task<IActionResult> CreateEvent([FromBody] CreateEventDTO createEventDto)
        {
            // Validate incoming request
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            // Map DTO to Domain Model
            var eventToCreate = mapper.Map<Event>(createEventDto);

            // Call repository to add the event to the database
            var createdEvent = await eventRepository.createEventAsync(eventToCreate);

            // Return the created event
            return CreatedAtAction(nameof(getEventById), new { id = createdEvent.Id }, mapper.Map<EventDTO>(createdEvent));
        }
    }
}