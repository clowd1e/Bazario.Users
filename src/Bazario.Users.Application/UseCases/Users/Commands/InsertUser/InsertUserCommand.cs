﻿using Bazario.AspNetCore.Shared.Domain.Common.Users.Roles;
using Bazario.AspNetCore.Shared.Results;
using MediatR;

namespace Bazario.Users.Application.UseCases.Users.Commands.InsertUser
{
    public sealed record InsertUserCommand(
        Guid UserId,
        string Email,
        string FirstName,
        string LastName,
        DateOnly BirthDate,
        string PhoneNumber,
        Role Role = Role.User) : IRequest<Result>;
}
