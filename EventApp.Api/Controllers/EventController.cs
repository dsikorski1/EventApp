using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EventApp.Infrastructure.Commands.Events;
using EventApp.Infrastructure.Services;
using Microsoft.AspNetCore.Mvc;

namespace EventApp.Api.Controllers
{
    [Route("events")]
    public class EventController : Controller
    {
        private readonly IEventService _service;

        public EventController(IEventService service)
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
        public async Task<IActionResult> Get(string id)
        {
            var @event = await _service.GetAsync(new Guid(id));

            return Json(@event);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody]CreateEvent command)
        {
            await _service.CreateAsync(command.EventId, command.Name, command.Description,
                command.StartDate, command.EndDate);
            await _service.AddTicketsAsync(command.EventId, command.Price);

            return Created($"/events/{command.EventId}", null);
        }
    }
}
