using System;

namespace EventApp.Infrastructure.DTO
{
    public class TicketDto
    {
        public Guid Id { get; set; }
        public Guid? UserId { get; set; }
        public decimal Price { get; set; }
        public int Seating { get; set; }
        public DateTime? PurchasedAt { get; set; }

        public bool Purchased()
        {
            return UserId.HasValue;
        }
    }
}
