using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using EventApp.Core.Domain;
using EventApp.Core.Repositories;
using EventApp.Infrastructure.DTO;
using EventApp.Infrastructure.Extensions;

namespace EventApp.Infrastructure.Services
{
    public class EventService : IEventService
    {
        private readonly IEventRepository _repository;
        private readonly IMapper _mapper;

        public EventService(IEventRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<EventDto> GetAsync(Guid id)
        {
            var @event = await _repository.GetAsync(id);

            return _mapper.Map<EventDto>(@event);
        }

        public async Task<EventDto> GetAsync(string name)
        {
            var @event = await _repository.GetAsync(name);

            return _mapper.Map<EventDto>(@event);
        }

        public async Task<IEnumerable<EventDto>> BrowseAsync()
        {
            var events = await _repository.BrowseAsync();

            return _mapper.Map<IEnumerable<EventDto>>(events);
        }

        public async Task CreateAsync(Guid id, string name, string description, DateTime startDate, DateTime endDate)
        {
            var @event = await _repository.GetAsync(name);
            if(@event != null)
            {
                throw new Exception($"Event named: '{name}' already exist.");
            }

            @event = new Event(id, name, description, startDate, endDate);
            await _repository.AddAsync(@event);
        }

        public async Task UpdateAsync(Guid id, string name, string description)
        {
            var @event = await _repository.GetOrFailAsync(id);
            if(await _repository.GetAsync(name) != null)
            {
                throw new Exception($"Event with name: '{name}' already exists.");
            }

            @event.SetName(name);
            @event.SetDescription(description);

            await _repository.UpdateAsync(@event);
        }

        public async Task DeleteAsync(Guid id)
        {
            await _repository.DeleteAsync(new Event(Guid.NewGuid(), "Event 1", "Description 1", DateTime.UtcNow, DateTime.UtcNow));
        }

        public async Task AddTicketsAsync(Guid id, int amount, decimal price)
        {
            var @event = await _repository.GetOrFailAsync(id);

            @event.AddTickets(amount, price);
        }
    }
}
