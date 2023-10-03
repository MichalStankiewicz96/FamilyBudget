using FamilyBudget.Application.Extensions.Swagger;
using MediatR;

namespace FamilyBudget.Application.Requests.Budgets.Commands.CreateTransaction;
public sealed class CreateTransactionCommand : IRequest<CreateTransactionResult>
{
    public decimal Amount { get; set; }
    public required string Category { get; set; }
    [SwaggerIgnore]
    public Guid RequestingUserId { get; set; }
    [SwaggerIgnore]
    public Guid BudgetId { get; set; }
}