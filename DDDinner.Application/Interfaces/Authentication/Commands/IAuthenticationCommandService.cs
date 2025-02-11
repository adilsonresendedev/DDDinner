using DDDinner.Application.Common;
using ErrorOr;

namespace DDDinner.Application.Interfaces.Authentication.Commands
{
    public interface IAuthenticationCommandService
    {
        Task<ErrorOr<AuthenticationResult>> Register(string firstName, string lastName, string email, string password);
    }
}