using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using BookingsApplication.API.DTOs;
using BookingsApplication.API.Repositories;
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
    }
}