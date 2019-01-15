using System;
using System.Collections.Generic;
using System.Linq;
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
        private readonly IMapper mapper;

        public TicketService(IUserRepository userRepository, IEventRepository eventRepository, IMapper mapper)
        {
            this.userRepository = userRepository;
            this.eventRepository = eventRepository;
            this.mapper = mapper;
        }

        public async Task<TicketDto> GetAsync(Guid eventId, Guid ticketId)
        {
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

        public async Task<IEnumerable<TicketDetailsDto>> GetTicketsPurchasedByUserAsync(Guid userId)
        {
            var tickets = new List<TicketDetailsDto>();
            var events = await eventRepository.BrowseAsync();
            foreach (var @event in events)
            {
                var userTickets = mapper.Map<IEnumerable<TicketDetailsDto>>(@event.GetTicketsPurchasedByUser(userId))
                    .ToList();

                userTickets.ForEach(t =>
                {
                    t.EventId = @event.Id;
                    t.EventName = @event.Name;
                });
                tickets.AddRange(userTickets);
            }

            return tickets;
        }
    }
}
