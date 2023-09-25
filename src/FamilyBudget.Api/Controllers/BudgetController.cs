using FamilyBudget.Api.Extensions.Auth;
using FamilyBudget.Application.Requests.Budgets.Commands.CreateBudget;
using FamilyBudget.Application.Requests.Budgets.Queries.GetUserBudgets;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FamilyBudget.Api.Controllers;

[ApiController]
[Produces("application/json")]
[Route("api/Budgets")]
[Authorize]
public class BudgetController : ControllerBase
{
    private readonly IMediator _mediator;

    public BudgetController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    [ProducesResponseType(typeof(CreateBudgetResult), StatusCodes.Status200OK)]
    public async Task<ActionResult> CreateBudgetAsync(CreateBudgetCommand request, CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(request, cancellationToken);
        return Ok(result);
    }

    [HttpGet]
    [ProducesResponseType(typeof(GetUserBudgetsQueryResponse), StatusCodes.Status200OK)]
    public async Task<ActionResult> GetUserBudgetsAsync([FromQuery] int? pageNumber, [FromQuery] int? pagerSize, CancellationToken cancellationToken)
    {
        var request = new GetUserBudgetsQuery
        {
            RequestingUserId = User.Claims.GetUserId(),
            PageNumber = pageNumber ?? 1,
            PageSize = pagerSize ?? 5
        };
        var response = await _mediator.Send(request, cancellationToken);
        return Ok(response);
    }
}
