using DDDinner.Application.Common.Services.Authentication;
using ErrorOr;

namespace DDDinner.Application.Common.Interfaces.Authentication
{
    public interface IAuthenticationService
    {
        Task<ErrorOr<AuthenticationResult>> Login(string email, string password);

        Task<ErrorOr<AuthenticationResult>> Register(string firstName, string lastName, string email, string password);
    }
}