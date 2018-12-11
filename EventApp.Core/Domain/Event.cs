using System;
using System.Collections.Generic;
using System.Linq;
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
        public IEnumerable<Ticket> PurchasedTickets => _tickets.Where(t => t.IsPurchased());
        public IEnumerable<Ticket> AvailableTickets => _tickets.Except(PurchasedTickets);

        protected Event() : base(Guid.NewGuid())
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
        }

        public void SetName(string name)
        {
            Name = name;
        }

        public void SetDescription(string description)
        {
            Description = description;
        }

        public void AddTickets(int amount, decimal price)
        {
            var seating = _tickets.Count + 1;
            for (var i = 0; i < amount; i++)
            {
                _tickets.Add(new Ticket(this, seating, price));
                seating++;
            }
        }

        public void PurchaseTickets(User user, int amount)
        {
            if (AvailableTickets.Count() < amount)
            {
                throw new Exception("Not enought available tickets to purchase.");
            }

            foreach (var ticket in AvailableTickets.Take(amount))
            {
                ticket.Purchase(user);
            }
        }

        public void CancelTickets(User user, int amount)
        {
            var tickets = PurchasedTickets.Where(t => t.UserId == user.Id);
            if (tickets.Count() < amount)
            {
                throw new Exception("Not enought purchased tickets to cancel.");
            }

            foreach (var ticket in tickets.Take(amount))
            {
                ticket.Cancel();
            }
        }
    }
}
