using DDDinner.Application.Common.Interfaces.Authentication;
using DDDinner.Contracts.Authentication;
using Microsoft.AspNetCore.Mvc;

namespace DDDinner.Api.Controllers
{
    [Route("[controller]")]
    public class AuthenticationController : ApiController
    {
        private readonly IAuthenticationService _authenticationService;
        private readonly ILogger<AuthenticationController> _logger;

        public AuthenticationController(
            IAuthenticationService authenticationService,
            ILogger<AuthenticationController> logger)
        {
            _authenticationService = authenticationService;
            _logger = logger;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody]RegisterRequest inbound)
        {
            var result = await _authenticationService.Register(inbound.FirstName, inbound.LastName, inbound.Email, inbound.Password);
            return HandleResult(result);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest inbound)
        {
            var result = await _authenticationService.Login(inbound.Email, inbound.Password);
            return HandleResult(result);
        }
    }
}