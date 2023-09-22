using FamilyBudget.Persistence.Entities.BudgetMembers;
using FamilyBudget.Persistence.Entities.Transaction;

namespace FamilyBudget.Persistence.Entities.Budgets;
public sealed class BudgetEntity
{
    public Guid Id { get; set; }
    public required string Description { get; set; }
    public ICollection<BudgetMemberEntity> BudgetMembers { get; set; } = new List<BudgetMemberEntity>();
    public ICollection<TransactionEntity> Transactions { get; set; } = new List<TransactionEntity>();
}