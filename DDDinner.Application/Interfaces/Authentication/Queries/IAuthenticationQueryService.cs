using DDDinner.Application.Common;
using ErrorOr;

namespace DDDinner.Application.Interfaces.Authentication.Queries
{
    public interface IAuthenticationQueryService
    {
        Task<ErrorOr<AuthenticationResult>> Login(string email, string password);
    }
}