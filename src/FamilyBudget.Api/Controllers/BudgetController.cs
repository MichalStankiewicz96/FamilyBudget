using FamilyBudget.Application.Requests.Budgets.Commands.CreateBudget;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace FamilyBudget.Api.Controllers;

[ApiController]
[Produces("application/json")]
[Route("api/Budgets")]
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
}
