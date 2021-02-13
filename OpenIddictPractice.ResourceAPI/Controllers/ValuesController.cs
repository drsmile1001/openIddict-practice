using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace OpenIddictPractice.ResourceAPI.Controllers
{
    [Route("api/[controller]")]
    [Authorize]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        [HttpGet]
        public object Get()
        {
            return new
            {
                User.Identity.IsAuthenticated,
                User.Identity.AuthenticationType,
                User.Identity.Name,
                Claims = User.Claims.Select(c => new
                {
                    c.Type,
                    c.Value
                }).ToArray()
            };
        }
    }
}
