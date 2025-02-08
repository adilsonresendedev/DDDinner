namespace DDDinner.Application.Common.Services.Authentication
{
    public interface IAuthenticationService
    {
        AuthenticationResult Login(string email, string password);

        AuthenticationResult Register(string firstName, string lastName, string email, string password);
    }
}