using BookingsApplication.API.Data;
using BookingsApplication.API.Models.Domains;
using Microsoft.EntityFrameworkCore;

namespace BookingsApplication.API.Repositories
{
    public class SQLEventRepository : IEventRepository
    {
        private readonly BookingAppDBcontext dBcontext;

        public SQLEventRepository(BookingAppDBcontext dBcontext)
        {
            this.dBcontext = dBcontext;
        }

       

        public async Task<List<Event>> getAllEventsAsync()
        {
            
            return await dBcontext.Events.Include(e=>e.TicketTypes).ToListAsync();
            
        }

        public Task<Event?> getEventByIdAsync(Guid id)
        {
            return dBcontext.Events.FirstOrDefaultAsync(x=>x.Id == id);
        }

        public async Task<Event> createEventAsync(Event inputEvent)
        {
            // Add the event to the DbContext
            await dBcontext.Events.AddAsync(inputEvent);
            await dBcontext.SaveChangesAsync(); // Commit to the database
            
            return inputEvent;
        }
    }
}
