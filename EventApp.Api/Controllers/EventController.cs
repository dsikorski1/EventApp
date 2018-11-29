using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
        public async Task<IActionResult> Get(string name)
        {
            var events = await _service.BrowseAsync();

            return Json(events);
        }
    }
}
