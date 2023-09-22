using FamilyBudget.Persistence.Entities.Budgets;
using FamilyBudget.Persistence.Entities.Users;

namespace FamilyBudget.Persistence.Entities.BudgetMembers;
public sealed class BudgetMemberEntity
{
    public Guid BudgetId { get; set; }
    public BudgetEntity? Budget { get; set; }
    public Guid UserId { get; set; }
    public UserEntity? User { get; set; }
}