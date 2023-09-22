namespace FamilyBudget.Application.Requests.Budgets.Queries.GetUserBudgets;
public sealed class GetUserBudgetsQueryResponse
{
    public BudgetDto[] Budgets { get; set; } = Array.Empty<BudgetDto>();
}