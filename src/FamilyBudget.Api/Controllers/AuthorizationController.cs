using FamilyBudget.Application.Requests.Authorization;
using FamilyBudget.Application.Requests.Authorization.Commands.GenerateJwt;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FamilyBudget.Api.Controllers;

[ApiController]
[Produces("application/json")]
[Route("api/Authorization")]
public class AuthorizationController : ControllerBase
{
    private readonly IMediator _mediator;

    public AuthorizationController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [AllowAnonymous]
    [HttpPost("JWT")]
    [ProducesResponseType(typeof(AuthenticationDto), StatusCodes.Status200OK)]
    public async Task<ActionResult> GetJwt(GenerateJwtCommand request, CancellationToken cancellationToken)
    {
        //this is a dirty hack to retrieve jwt. Normally we check if login and password are valid
        var result = await _mediator.Send(request, cancellationToken);
        return Ok(result);
    }
}
