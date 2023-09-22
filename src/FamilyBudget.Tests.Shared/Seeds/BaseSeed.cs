using FamilyBudget.Persistence;

namespace FamilyBudget.Tests.Shared.Seeds;

public abstract class BaseSeed
{
    protected readonly ApplicationDbContext Context;

    protected BaseSeed(ApplicationDbContext context)
    {
        Context = context;
    }

    public abstract Task SeedAsync();
}