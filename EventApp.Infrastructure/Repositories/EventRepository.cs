using EventApp.Core.Domain;
using EventApp.Core.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventApp.Infrastructure.Repositories
{
    public class EventRepository : IEventRepository
    {
        private ISet<Event> _events = new HashSet<Event>();

        public async Task<Event> GetAsync(Guid id)
        {
            var result = _events.SingleOrDefault(e => e.Id == id);

            return await Task.FromResult(result);
        }

        public async Task<Event> GetAsync(string name)
        {
            var result = _events.SingleOrDefault(e => e.Name.ToLowerInvariant() == name.ToLowerInvariant());

            return await Task.FromResult(result);
        }

        public async Task<IEnumerable<Event>> BrowseAsync()
        {
            var events = _events.AsEnumerable();

            return await Task.FromResult(_events);
        }

        public async Task AddAsync(Event @event)
        {
            _events.Add(@event);
            await Task.CompletedTask;
        }

        public async Task UpdateAsync(Event @event)
        {
            await Task.CompletedTask;
        }

        public async Task DeleteAsync(Event @event)
        {
            _events.Remove(@event);
            await Task.CompletedTask;
        }
    }
}
