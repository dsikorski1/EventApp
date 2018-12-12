using System;
using System.Collections.Generic;
using System.Text;

namespace EventApp.Infrastructure.Commands.Tickets
{
    public class CancelTicket
    {
        public Guid TicketId { get; set; }
        public Guid EventId { get; set; }
        public int Amount { get; set; }

        public CancelTicket()
        {
        }
    }
}
