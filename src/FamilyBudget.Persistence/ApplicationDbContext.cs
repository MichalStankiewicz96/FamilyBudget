using FamilyBudget.Persistence.Entities.BudgetMembers;
using FamilyBudget.Persistence.Entities.Budgets;
using FamilyBudget.Persistence.Entities.Transaction;
using FamilyBudget.Persistence.Entities.Users;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace FamilyBudget.Persistence;
public sealed class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

    public DbSet<UserEntity> Users => Set<UserEntity>();
    public DbSet<TransactionEntity> Transactions => Set<TransactionEntity>();
    public DbSet<BudgetEntity> Budgets => Set<BudgetEntity>();
    public DbSet<BudgetMemberEntity> BudgetMembers => Set<BudgetMemberEntity>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        base.OnModelCreating(modelBuilder);
    }
}