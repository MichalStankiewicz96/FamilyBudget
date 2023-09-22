using MediatR;

namespace FamilyBudget.Application.Requests.Budgets.Queries.GetUserBudgets;
public sealed class GetUserBudgetsQuery : IRequest<GetUserBudgetsQueryResponse>
{
    public Guid RequestingUserId { get; set; }
}
