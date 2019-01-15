using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using EventApp.Infrastructure.DTO;

namespace EventApp.Infrastructure.Services
{
    public interface IEventService
    {
        Task<EventDetailsDto> GetAsync(Guid id);
        Task<EventDetailsDto> GetAsync(string name);
        Task<IEnumerable<EventDto>> BrowseAsync();
        Task CreateAsync(Guid id, string name, string description, DateTime startDate, DateTime endDate);
        Task UpdateAsync(Guid id, string name, string description);
        Task DeleteAsync(Guid id);
        Task AddTicketsAsync(Guid id, int amount, decimal price);
    }
}
