using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EventApp.Infrastructure.Commands.Tickets;
using EventApp.Infrastructure.DTO;
using EventApp.Infrastructure.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EventApp.Api.Controllers
{
    [Route("[controller]")]
    [ApiController]
    [Authorize]
    public class TicketsController : ApiController
    {
        private readonly ITicketService ticketService;

        public TicketsController(ITicketService ticketService)
        {
            this.ticketService = ticketService;
        }

        [HttpGet("{eventId}/{ticketId}")]
        public async Task<IActionResult> Get(Guid eventId, Guid ticketId)
        {
            var ticket = await ticketService.GetAsync(UserId(), eventId, ticketId);

            return Json(ticket);
        }

        [HttpPost]
        public async Task<IActionResult> Purchase([FromBody]PurchaseTicket command)
        {
            await ticketService.PurchaseAsync(UserId(), command.EventId, command.Amount);

            return NoContent();
        }

        [HttpDelete]
        public async Task<IActionResult> Cancel([FromBody]CancelTicket command)
        {
            await ticketService.CancelAsync(UserId(), command.EventId, command.Amount);

            return NoContent();
        }
    }
}
