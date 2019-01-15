using System.Threading.Tasks;
using EventApp.Infrastructure.Commands.Users;
using EventApp.Infrastructure.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EventApp.Api.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class AccountController : ApiController
    {
        private readonly IUserService _service;

        public AccountController(IUserService service)
        {
            _service = service;
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> Get()
        {
            var user = await _service.GetAsync(UserId());

            return Json(user);
        }

        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login([FromBody] LoginUser command)
        {
            var token = await _service.LoginAsync(command);

            return Json(token);
        }

        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> Register([FromBody] RegisterUser command)
        {
            await _service.RegisterAsync(command);

            return Created("/account/login", null);
        }
    }
}
