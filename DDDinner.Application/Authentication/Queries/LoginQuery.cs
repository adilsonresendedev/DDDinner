using DDDinner.Application.Common;
using ErrorOr;
using MediatR;

namespace DDDinner.Application.Authentication.Queries
{
    public record LoginQuery(
        string Email,
        string Password) : IRequest<ErrorOr<AuthenticationResult>>;
}
