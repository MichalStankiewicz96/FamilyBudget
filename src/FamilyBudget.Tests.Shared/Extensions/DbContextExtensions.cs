using FamilyBudget.Persistence;
using FamilyBudget.Tests.Shared.Seeds;

namespace FamilyBudget.Tests.Shared.Extensions;
public static class DbContextExtensions
{
    public static async Task<ApplicationDbContext> SeedWithAsync<TSeed>(this ApplicationDbContext context)
        where TSeed : BaseSeed
    {
        var seeder = (TSeed?)Activator.CreateInstance(typeof(TSeed), context);
        if (seeder is null)
        {
            throw new InvalidOperationException();
        }
        await seeder.SeedAsync();
        context.ChangeTracker.Clear();
        return context;
    }
}