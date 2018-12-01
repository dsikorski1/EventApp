using System;
using System.Collections.Generic;
using System.Text;

namespace EventApp.Core.Domain
{
    public class Ticket : Entity
    {
        public Guid EventId { get; protected set; }
        public Guid? UserId { get; protected set; }
        public decimal Price { get; protected set; }
        public int Seating { get; protected set; }
        public DateTime? PurchasedAt { get; protected set; }

        protected Ticket() : base(Guid.NewGuid())
        {
        }

        public Ticket(Event @event, int setating, decimal price): base(Guid.NewGuid())
        {
            EventId = @event.Id;
            Price = price;
            Seating = setating;
        }

        public bool Purchased()
        {
            return UserId.HasValue;
        }
    }
}
