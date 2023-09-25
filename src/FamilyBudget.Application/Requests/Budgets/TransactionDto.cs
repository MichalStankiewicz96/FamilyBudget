namespace FamilyBudget.Application.Requests.Budgets;
public sealed class TransactionDto
{
    public Guid Id { get; set; }
    public Guid BudgetId { get; set; }
    public decimal Amount { get; set; }
    public required string Category { get; set; }
}