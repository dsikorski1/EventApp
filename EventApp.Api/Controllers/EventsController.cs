using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EventApp.Infrastructure.Commands.Events;
using EventApp.Infrastructure.Services;
using Microsoft.AspNetCore.Mvc;

namespace EventApp.Api.Controllers
{
    [Route("[controller]")]
    public class EventsController : Controller
    {
        private readonly IEventService _service;

        public EventsController(IEventService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> List()
        {
            var events = await _service.BrowseAsync();

            return Json(events);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(Guid id)
        {
            var @event = await _service.GetAsync(id);

            return Json(@event);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody]CreateEvent command)
        {
            await _service.CreateAsync(command.EventId, command.Name, command.Description,
                command.StartDate, command.EndDate);
            await _service.AddTicketsAsync(command.EventId, command.Tickets ,command.Price);

            return Created($"/events/{command.EventId}", null);
        }

        [HttpPut("{eventId}")]
        public async Task<IActionResult> Update(Guid eventId, [FromBody]UpdateCommand command)
        {
            await _service.UpdateAsync(eventId, command.Name, command.Description);

            return NoContent();
        }
    }
}
