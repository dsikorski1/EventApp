﻿using System;
using System.Threading.Tasks;
using EventApp.Infrastructure.Commands.Events;
using EventApp.Infrastructure.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EventApp.Api.Controllers
{
    [Route("[controller]")]
    [ApiController]
    [Authorize(Policy = "IsAdmin")]
    public class EventsController : ApiController
    {
        private readonly IEventService _service;

        public EventsController(IEventService service)
        {
            _service = service;
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> List()
        {
            var events = await _service.BrowseAsync();

            return Json(events);
        }

        [HttpGet("{eventId}")]
        public async Task<IActionResult> Get(Guid eventId)
        {
            var @event = await _service.GetAsync(eventId);
            if (@event == null)
            {
                return NotFound();
            }

            return Json(@event);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateEvent command)
        {
            await _service.CreateAsync(command.EventId, command.Name, command.Description,
                command.StartDate, command.EndDate);
            await _service.AddTicketsAsync(command.EventId, command.Tickets, command.Price);

            return Created($"/events/{command.EventId.ToString()}", null);
        }

        [HttpPut("{eventId}")]
        public async Task<IActionResult> Update(Guid eventId, [FromBody] UpdateCommand command)
        {
            await _service.UpdateAsync(eventId, command.Name, command.Description);

            return NoContent();
        }

        [HttpDelete("{eventId}")]
        public async Task<IActionResult> Delete(Guid eventId)
        {
            await _service.DeleteAsync(eventId);

            return NoContent();
        }
    }
}
