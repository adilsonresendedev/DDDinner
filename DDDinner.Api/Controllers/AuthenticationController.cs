using AutoMapper;
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
        private readonly IMapper _mapper;

        public AuthenticationController(ISender sender, IMapper mapper)
        {
            _sender = sender;
            _mapper = mapper;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody]RegisterRequest inbound)
        {

            var registerCommand = _mapper.Map<RegisterCommand>(inbound);
            var result = await _sender.Send(registerCommand);
            return HandleResult(result);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest inbound)
        {
            var loginQuery = _mapper.Map<LoginQuery>(inbound);
            var result = await _sender.Send(loginQuery);
            return HandleResult(result);
        }
    }
}