using DDDinner.Application.Authentication.Commands.Register;
using DDDinner.Application.Authentication.Queries;
using DDDinner.Contracts.Authentication;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace DDDinner.Api.Controllers
{
    [Route("[controller]")]
    public class AuthenticationController : ApiController
    {
        private readonly ISender _sender;

        public AuthenticationController(ISender sender)
        {
            _sender = sender;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody]RegisterRequest inbound)
        {
            var registerCommand = new RegisterCommand(inbound.FirstName, inbound.LastName, inbound.Email, inbound.Password);
            var result = await _sender.Send(registerCommand);
            return HandleResult(result);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest inbound)
        {
            var loginQuery = new LoginQuery(inbound.Email, inbound.Password);
            var result = await _sender.Send(loginQuery);
            return HandleResult(result);
        }
    }
}