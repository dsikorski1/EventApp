using Microsoft.AspNetCore.Mvc;
using System;

namespace EventApp.Api.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ApiController : Controller
    {
        protected Guid UserId()
        {
            if (User?.Identity?.IsAuthenticated == true)
            {
                return Guid.Parse(User.Identity.Name);
            }

            return Guid.Empty;
        }
    }
}
