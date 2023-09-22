namespace FamilyBudget.Application.Requests.Budgets;
public sealed class BudgetDto
{
    public Guid Id { get; set; }
    public required string Description { get; set; }
}