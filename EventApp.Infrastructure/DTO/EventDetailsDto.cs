using System;
using System.Collections.Generic;
using System.Text;

namespace EventApp.Infrastructure.DTO
{
    public class EventDetailsDto : EventDto
    {
        public IEnumerable<TicketDto> Tickets { get; set; }
    }
}
