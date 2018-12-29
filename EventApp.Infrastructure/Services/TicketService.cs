using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using EventApp.Core.Repositories;
using EventApp.Infrastructure.DTO;
using EventApp.Infrastructure.Extensions;

namespace EventApp.Infrastructure.Services
{
    public class TicketService : ITicketService
    {
        private readonly IUserRepository userRepository;
        private readonly IEventRepository eventRepository;
        private IMapper mapper;

        public TicketService(IUserRepository userRepository, IEventRepository eventRepository, IMapper mapper)
        {
            this.userRepository = userRepository;
            this.eventRepository = eventRepository;
            this.mapper = mapper;
        }

        public async Task<TicketDto> GetAsync(Guid userId, Guid eventId, Guid ticketId)
        {
            var user = await userRepository.GetOrFailAsync(userId);
            var ticket = await eventRepository.GetTicketOrFailAsync(eventId, ticketId);

            return mapper.Map<TicketDto>(ticket);
        }

        public async Task PurchaseAsync(Guid userId, Guid eventId, int amount)
        {
            var user = await userRepository.GetOrFailAsync(userId);
            var @event = await eventRepository.GetOrFailAsync(eventId);

            @event.PurchaseTickets(user, amount);
            await eventRepository.UpdateAsync(@event);

            await Task.CompletedTask;
        }

        public async Task CancelAsync(Guid userId, Guid eventId, int amount)
        {
            var user = await userRepository.GetOrFailAsync(userId);
            var @event = await eventRepository.GetOrFailAsync(eventId);

            @event.CancelTickets(user, amount);
            await eventRepository.UpdateAsync(@event);
      
            await Task.CompletedTask;
        }

        public async Task<IEnumerable<TicketDto>> GetTicketsPurchasedByUserAsync(Guid userId)
        {
            var user = await userRepository.GetOrFailAsync(userId);
            var events = await eventRepository.BrowseAsync();
            var tickets = events.SelectMany(x => x.GetTicketsPurchasedByUser(user));

            return mapper.Map<IEnumerable<TicketDto>>(tickets);
        }
    }
}
