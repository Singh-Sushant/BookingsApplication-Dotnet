using BookingsApplication.API.Models.Domains;

namespace BookingsApplication.API.Repositories
{
    public interface IEventRepository
    {
        Task<List<Event>> getAllEventsAsync();

        Task<Event?> getEventByIdAsync(Guid id);
    
        Task<Event> createEventAsync(Event inputEvent); 
    }
}
