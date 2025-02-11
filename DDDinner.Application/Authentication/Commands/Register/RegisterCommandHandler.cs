using DDDinner.Application.Common;
using DDDinner.Application.Persistence;
using DDDinner.Domain.Common.Errors;
using DDDinner.Domain.Entities;
using ErrorOr;
using MediatR;

namespace DDDinner.Application.Authentication.Commands.Register
{
    public class RegisterCommandHandler : IRequestHandler<RegisterCommand, ErrorOr<AuthenticationResult>>
    {
        private readonly IUserRepository _userRepository;
        private readonly IPasswordUtility _passwordUtility;

        public RegisterCommandHandler(IUserRepository userRepository, IPasswordUtility passwordUtility)
        {
            _userRepository = userRepository;
            _passwordUtility = passwordUtility;
        }

        public async Task<ErrorOr<AuthenticationResult>> Handle(RegisterCommand request, CancellationToken cancellationToken)
        {
            var existingUser = await _userRepository.GetUserByEmail(request.Email);
            if (existingUser != null)
            {
                return Errors.User.EmailDuplicated;
            }

            var passwordHashDto = _passwordUtility.CreatePasswordDto(request.Password);

            var user = new User
            {
                Id = Guid.NewGuid(),
                FirstName = request.FirstName,
                LastName = request.LastName,
                Email = request.Email,
                PasswordHash = passwordHashDto.PassworHash,
                PasswordSalt = passwordHashDto.PasswordSalt

            };

            await _userRepository.Add(user);
            var token = _passwordUtility.GenerateToken(user);
            return new AuthenticationResult(
                user.Id,
                user.FirstName,
                user.LastName,
                user.Email,
                token);
        }
    }
}
