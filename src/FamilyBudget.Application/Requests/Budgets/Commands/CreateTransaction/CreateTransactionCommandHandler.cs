using FamilyBudget.Application.Behaviour.Exceptions;
using FamilyBudget.Persistence;
using FamilyBudget.Persistence.Entities.Transaction;
using FamilyBudget.Persistence.Entities.Users;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FamilyBudget.Application.Requests.Budgets.Commands.CreateTransaction;
public sealed class CreateTransactionCommandHandler : IRequestHandler<CreateTransactionCommand, CreateTransactionResult>
{
    private readonly ApplicationDbContext _context;

    public CreateTransactionCommandHandler(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<CreateTransactionResult> Handle(CreateTransactionCommand request, CancellationToken cancellationToken)
    {
        var budget = await _context.Budgets
            .Include(x => x.BudgetMembers)
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.Id == request.BudgetId, cancellationToken);
        if (budget is null)
        {
            throw new NotFoundException(typeof(UserEntity));
        }
        var transaction = new TransactionEntity
        {
            Amount = request.Amount,
            BudgetId = request.BudgetId,
            Category = request.Category,
            Id = Guid.NewGuid()
        };
        _context.Transactions.Add(transaction);
        await _context.SaveChangesAsync(cancellationToken);
        return new CreateTransactionResult { Id = transaction.Id };
    }
}

