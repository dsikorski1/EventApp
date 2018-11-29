using System;
using System.Collections.Generic;
using System.Text;

namespace EventApp.Core.Domain
{
    public class Event : Entity
    {
        private ISet<Ticket> _tickets = new HashSet<Ticket>();

        public string Name { get; protected set; }
        public string Description { get; protected set; }
        public DateTime StartDate { get; protected set; }
        public DateTime EndDate { get; protected set; }
        public DateTime UpdatedAt { get; protected set; }
        public IEnumerable<Ticket> Tickets => _tickets;

        public Event() : base()
        {
        }

        public Event(Guid guid) : base(guid)
        {
        }

        public void AddTicket(Ticket ticket)
        {
            _tickets.Add(ticket);
        }
    }
}
