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

        protected Event() : base()
        {
        }

        public Event(Guid guid) : base(guid)
        {
        }

        public Event(Guid guid, string name, string description,
            DateTime startDate, DateTime endDate) : base(guid)
        {
            Name = name;
            Description = description;
            StartDate = startDate;
            EndDate = endDate;
            UpdatedAt = DateTime.UtcNow;
            SetCreatedAt();
        }

        public void AddTicket(Ticket ticket)
        {
            _tickets.Add(ticket);
        }
    }
}
