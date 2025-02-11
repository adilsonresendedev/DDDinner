namespace DDDinner.Application.Common
{
    public record AuthenticationResult(
        Guid Id,
        string FirstName,
        string LastName,
        string Email,
        string token
    );
}