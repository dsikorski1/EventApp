using System;
using System.Collections.Generic;
using System.Text;

namespace EventApp.Infrastructure.Commands.Tickets
{
    public class PurchaseTicket
    {
        public Guid EventId { get; set; }
        public int Amount { get; set; }

        public PurchaseTicket()
        {
        }
    }
}
