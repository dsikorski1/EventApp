using EventApp.Infrastructure.DTO;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EventApp.Infrastructure.Services
{
    public interface IEventService
    {
        Task<EventDto> GetAsync(Guid id);
        Task<EventDto> GetAsync(string name);
        Task<IEnumerable<EventDto>> BrowseAsync();
        Task CreateAsync(Guid guid, string name, string description, DateTime startDate, DateTime endDate);
        Task UpdateAsync(Guid guid, string name, string description);
        Task DeleteAsync(Guid guid);
    }
}
