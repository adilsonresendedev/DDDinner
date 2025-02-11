using DDDinner.Application.Common;
using DDDinner.Application.Persistence;
using DDDinner.Domain.Common.Errors;
using ErrorOr;
using MediatR;

namespace DDDinner.Application.Authentication.Queries
{
    public class LoginQueryHandler : IRequestHandler<LoginQuery, ErrorOr<AuthenticationResult>>
    {
        private readonly IUserRepository _userRepository;
        private readonly IPasswordUtility _passwordUtility;

        public LoginQueryHandler(IUserRepository userRepository, IPasswordUtility passwordUtility)
        {
            _userRepository = userRepository;
            _passwordUtility = passwordUtility;
        }

        public async Task<ErrorOr<AuthenticationResult>> Handle(LoginQuery request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetUserByEmail(request.Email);
            if (user == null)
            {
                return Errors.User.EmailNotFound;
            }

            var passwordCheckResult = _passwordUtility.CheckHash(request.Password, user.PasswordHash, user.PasswordSalt);
            if (!passwordCheckResult)
            {
                return Errors.User.Unauthorized;
            }

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
