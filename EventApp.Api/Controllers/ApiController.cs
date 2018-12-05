using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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
