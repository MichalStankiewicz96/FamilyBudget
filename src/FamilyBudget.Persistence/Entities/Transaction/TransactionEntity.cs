using FamilyBudget.Persistence.Entities.Budgets;

namespace FamilyBudget.Persistence.Entities.Transaction;
public sealed class TransactionEntity
{
    public Guid Id { get; set; }
    public Guid BudgetId { get; set; }
    public BudgetEntity? Budget { get; set; }
    public decimal Amount { get; set; }
    public required string Category { get; set; }
}