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

        public Ticket() : base()
        {
        }

        public Ticket(Guid guid) : base(guid)
        {
        }

        public Ticket(Guid eventId, decimal price): base(Guid.NewGuid())
        {
            EventId = eventId;
            Price = price;
            Seating = 1;
            PurchasedAt = DateTime.UtcNow;
        }

        public bool Purchased()
        {
            return UserId.HasValue;
        }
    }
}
