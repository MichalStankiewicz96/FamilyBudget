using FamilyBudget.Application.Behaviour.Pagination;
using FamilyBudget.Persistence;
using FamilyBudget.Persistence.Entities.Budgets;
using MapsterMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FamilyBudget.Application.Requests.Budgets.Queries.GetUserBudgets;
public sealed class GetUserBudgetsQueryHandler : IRequestHandler<GetUserBudgetsQuery, GetUserBudgetsQueryResponse>
{
    private readonly ApplicationDbContext _context;
    private readonly IMapper _mapper;

    public GetUserBudgetsQueryHandler(ApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<GetUserBudgetsQueryResponse> Handle(GetUserBudgetsQuery request, CancellationToken cancellationToken)
    {
        var budgetEntities = _context.BudgetMembers.Where(x => x.UserId == request.RequestingUserId)
            .Include(x => x.Budget)
            .ThenInclude(b => b!.Transactions)
            .AsNoTracking()
            .Select(x => x.Budget)
            .OrderBy(x => x!.Description);
        var budgets = new List<BudgetDto>();
        var pagedBudgets = PagedResponse<BudgetEntity>.ToPaged(budgetEntities!, request.PageNumber, request.PageSize);
        foreach (var budget in pagedBudgets)
        {
            var groupedIncome = budget.Transactions.Where(x => x.Amount > 0).GroupBy(x => x.Category).ToList();
            var groupedExpenses = budget.Transactions.Where(x => x.Amount < 0).GroupBy(x => x.Category).ToList();
            var budgetDto = _mapper.Map<BudgetDto>(budget);
            budgetDto.Income = groupedIncome.Select(x => new BudgetGroupedTransactionDto
            {
                Category = x.Key,
                Transactions = _mapper.Map<TransactionDto[]>(x.ToArray())
            }).ToArray();
            budgetDto.Expenses = groupedExpenses.Select(x => new BudgetGroupedTransactionDto
            {
                Category = x.Key,
                Transactions = _mapper.Map<TransactionDto[]>(x.ToArray())
            }).ToArray();
            budgets.Add(budgetDto);
        }
        var response = new GetUserBudgetsQueryResponse
        {
            CurrentPage = pagedBudgets.CurrentPage,
            TotalPages = pagedBudgets.TotalPages,
            PageSize = pagedBudgets.PageSize,
            TotalCount = pagedBudgets.TotalCount,
            HasPrevious = pagedBudgets.HasPrevious,
            HasNext = pagedBudgets.HasNext,
            Budgets = budgets.ToArray()
        };
        return response;
    }
}
