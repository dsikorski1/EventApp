using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using EventApp.Core.Domain;
using EventApp.Core.Repositories;
using EventApp.Infrastructure.DTO;

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

        public async Task CreateAsync(Guid guid, string name, string description, DateTime startDate, DateTime endDate)
        {
            await _repository.AddAsync(new Event());
        }

        public async Task UpdateAsync(Guid guid, string name, string description)
        {
            await _repository.UpdateAsync(new Event());
        }

        public async Task DeleteAsync(Guid guid)
        {
            await _repository.DeleteAsync(new Event());
        }
    }
}
