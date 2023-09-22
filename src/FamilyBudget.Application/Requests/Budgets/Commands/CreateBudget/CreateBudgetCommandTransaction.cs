namespace FamilyBudget.Application.Requests.Budgets.Commands.CreateBudget;
public sealed class CreateBudgetCommandTransaction
{
    public decimal Amount { get; set; }
    public required string Category { get; set; }
}