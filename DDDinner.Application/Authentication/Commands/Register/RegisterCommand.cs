﻿using DDDinner.Application.Common;
using ErrorOr;
using MediatR;

namespace DDDinner.Application.Authentication.Commands.Register
{
    public record RegisterCommand(
        string FirstName,
        string LastName,
        string Email,
        string Password) : IRequest<ErrorOr<AuthenticationResult>>;
}
