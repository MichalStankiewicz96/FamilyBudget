﻿using FamilyBudget.Application.Requests.Users.Commands.CreateUser;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FamilyBudget.Api.Controllers;

[ApiController]
[Produces("application/json")]
[Route("api/Users")]
public class UsersController : ControllerBase
{
    private readonly IMediator _mediator;

    public UsersController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [AllowAnonymous]
    [HttpPost]
    [ProducesResponseType(typeof(CreateUserCommandResult), StatusCodes.Status200OK)]
    public async Task<ActionResult> CreateAsync(CreateUserCommand request, CancellationToken cancellationToken)
    {
        var userId = await _mediator.Send(request, cancellationToken);
        return Ok(new CreateUserCommandResult { UserId = userId });
    }
}
