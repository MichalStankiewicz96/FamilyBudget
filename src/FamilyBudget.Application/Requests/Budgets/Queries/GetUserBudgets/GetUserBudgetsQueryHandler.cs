using FamilyBudget.Persistence;
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
        var budgets = await _context.BudgetMembers.Where(x => x.UserId == request.RequestingUserId)
            .Include(x => x.Budget)
            .AsNoTracking()
            .Select(x => x.Budget).ToListAsync(cancellationToken);
        var response = new GetUserBudgetsQueryResponse
        {
            Budgets = _mapper.Map<BudgetDto[]>(budgets)
        };
        return response;
    }
}

