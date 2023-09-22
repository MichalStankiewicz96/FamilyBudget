using FamilyBudget.Application.Behaviour.Exceptions;
using FamilyBudget.Persistence;
using FamilyBudget.Persistence.Entities.BudgetMembers;
using FamilyBudget.Persistence.Entities.Budgets;
using FamilyBudget.Persistence.Entities.Transaction;
using FamilyBudget.Persistence.Entities.Users;
using MediatR;
using Microsoft.EntityFrameworkCore;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace FamilyBudget.Application.Requests.Budgets.Commands.CreateBudget;
public sealed class CreateBudgetCommandHandler : IRequestHandler<CreateBudgetCommand, CreateBudgetResult>
{
    private readonly ApplicationDbContext _context;

    public CreateBudgetCommandHandler(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<CreateBudgetResult> Handle(CreateBudgetCommand request, CancellationToken cancellationToken)
    {
        if (!await UsersExistsAsync(request.MemberIds, cancellationToken))
        {
            throw new NotFoundException(typeof(UserEntity));
        }
        var budgetId = Guid.NewGuid();
        var budgetEntity = new BudgetEntity
        {
            Id = budgetId,
            Description = request.Description,
            BudgetMembers = PrepareBudgetMembers(budgetId, request.RequestingUserId, request.MemberIds),
            Transactions = PrepareTransactions(budgetId, request.Incomes, request.Expenses)
        };
        _context.Budgets.Add(budgetEntity);
        await _context.SaveChangesAsync(cancellationToken);
        return new CreateBudgetResult { Id = budgetId };
    }

    private static List<BudgetMemberEntity> PrepareBudgetMembers(Guid budgetId, Guid requestingUserId, ICollection<Guid> userIds)
    {
        var allMemberIds = userIds.Concat(new List<Guid> { requestingUserId });
        return allMemberIds.Distinct().Select(userId => new BudgetMemberEntity
        {
            BudgetId = budgetId,
            UserId = userId
        }).ToList();
    }

    private static List<TransactionEntity> PrepareTransactions(Guid budgetId, ICollection<CreateBudgetCommandTransaction> incomes, ICollection<CreateBudgetCommandTransaction> expenses)
    {
        var transactions = PrepareTransactions(budgetId, incomes);
        transactions.AddRange(PrepareTransactions(budgetId, expenses));
        return transactions;
    }

    private static List<TransactionEntity> PrepareTransactions(Guid budgetId, ICollection<CreateBudgetCommandTransaction> transactions)
    {
        return transactions.Select(x => new TransactionEntity
        {
            Id = Guid.NewGuid(),
            BudgetId = budgetId,
            Amount = x.Amount,
            Category = x.Category
        }).ToList();
    }

    private async Task<bool> UsersExistsAsync(ICollection<Guid> userIds, CancellationToken cancellationToken)
    {
        var existingUsersCount = await _context.Users.CountAsync(x => userIds.Contains(x.Id), cancellationToken);
        return existingUsersCount == userIds.Distinct().Count();
    }
}