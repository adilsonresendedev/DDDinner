using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DDDinner.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class DinnersController : ApiController
    {
        [HttpGet]
        public async Task<IActionResult> GetDinners()
        {
            return Ok("Dinner");
        }
    }
}
