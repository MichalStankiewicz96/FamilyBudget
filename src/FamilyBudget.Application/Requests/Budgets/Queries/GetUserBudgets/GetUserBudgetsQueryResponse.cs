using FamilyBudget.Application.Behaviour.Pagination;

namespace FamilyBudget.Application.Requests.Budgets.Queries.GetUserBudgets;
public sealed class GetUserBudgetsQueryResponse : PagedResponse
{
    public BudgetDto[] Budgets { get; set; } = Array.Empty<BudgetDto>();
}