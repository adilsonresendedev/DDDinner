using DDDinner.Application.Common.Interfaces.Authentication;
using DDDinner.Application.Common.Persistence;
using DDDinner.Domain.Common.Errors;
using DDDinner.Domain.Entities;
using ErrorOr;

namespace DDDinner.Application.Common.Services.Authentication
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly IUserRepository _userRepository;
        private readonly IPasswordUtility _jwtTokenGenerator;

        public AuthenticationService(
            IPasswordUtility jwtTokenGenerator,
            IUserRepository userRepository)
        {
            _jwtTokenGenerator = jwtTokenGenerator;
            _userRepository = userRepository;
        }

        public async Task<ErrorOr<AuthenticationResult>> Login(string email, string password)
        {
            var user = await _userRepository.GetUserByEmail(email);
            if (user == null)
            {
                return Errors.User.EmailNotFound;
            }

            var passwordCheckResult = _jwtTokenGenerator.CheckHash(password, user.PasswordHash, user.PasswordSalt);
            if (!passwordCheckResult)
            {
                return Errors.User.Unauthorized;
            }

            var token = _jwtTokenGenerator.GenerateToken(user);
            return new AuthenticationResult(
                user.Id,
                user.FirstName,
                user.LastName,
                email,
                token);
        }

        public async Task<ErrorOr<AuthenticationResult>> Register(string firstName, string lastName, string email, string password)
        {
            var existingUser = await _userRepository.GetUserByEmail(email);
            if (existingUser != null)
            {
                return Errors.User.EmailDuplicated;
            }

            var passwordHashDto = _jwtTokenGenerator.CreatePasswordDto(password);

            var user = new User
            {
                Id = Guid.NewGuid(),
                FirstName = firstName,
                LastName = lastName,
                Email = email,
                PasswordHash = passwordHashDto.PassworHash,
                PasswordSalt = passwordHashDto.PasswordSalt

            };

            await _userRepository.Add(user);
            var token = _jwtTokenGenerator.GenerateToken(user);
            return new AuthenticationResult(
                user.Id,
                user.FirstName,
                user.LastName,
                user.Email,
                token);
        }
    }
}