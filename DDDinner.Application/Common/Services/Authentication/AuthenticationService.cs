using DDDinner.Application.Common.Interfaces.Authentication;
using DDDinner.Application.Common.Persistence;
using DDDinner.Domain.Entities;

namespace DDDinner.Application.Common.Services.Authentication
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly IUserRepository _userRepository;
        private readonly IJwtTokenGenerator _jwtTokenGenerator;

        public AuthenticationService(
            IJwtTokenGenerator jwtTokenGenerator,
            IUserRepository userRepository)
        {
            _jwtTokenGenerator = jwtTokenGenerator;
            _userRepository = userRepository;
        }

        public AuthenticationResult Login(string email, string password)
        {
            var user = _userRepository.GetUserByEmail(email);
            if (user == null)
            {
                throw new Exception("User doesn't exist .");
            }

            var token = _jwtTokenGenerator.GenerateToken(user);
            return new AuthenticationResult(
                user.Id,
                user.FirstName,
                user.LastName,
                email,
                token);
        }

        public AuthenticationResult Register(string firstName, string lastName, string email, string password)
        {
            var existingUser = _userRepository.GetUserByEmail(email);
            if (existingUser != null)
            {
                throw new Exception("User already exists.");
            }

            var user = new User
            {
                Id = Guid.NewGuid(),
                FirstName = firstName,
                LastName = lastName,
                Email = email,
                Password = password

            };

            _userRepository.Add(user);
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