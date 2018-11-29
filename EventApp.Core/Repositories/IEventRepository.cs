using EventApp.Core.Domain;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EventApp.Core.Repositories
{
    public interface IEventRepository
    {
        Task<Event> GetAsync(Guid id);
        Task<Event> GetAsync(string name);
        Task<IEquatable<Event>> BrowseAsync();
        Task AddAsync(Domain.Event @event);
        Task UpdateAsync(Domain.Event @event);
        Task DeleteAsync(Domain.Event @event);
    }
}
