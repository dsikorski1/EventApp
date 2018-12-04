using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EventApp.Infrastructure.Commands.Users;
using EventApp.Infrastructure.Services;
using Microsoft.AspNetCore.Mvc;

namespace EventApp.Api.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class AccountController : Controller
    {
        private IUserService _service;

        public AccountController(IUserService service)
        {
            _service = service;        
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(Guid id)
        {
            var user = await _service.GetAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            return Json(user);
        }

        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login([FromBody]LoginUser command)
        {
            var user = await _service.LoginAsync(command);

            return Json(User);
        }

        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> Register([FromBody]RegisterUser command)
        {
            await _service.RegisterAsync(command);

            return Created($"acount/{command.Id}", command);
        }
    }
}
