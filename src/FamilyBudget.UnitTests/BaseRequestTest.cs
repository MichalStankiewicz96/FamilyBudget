using FamilyBudget.Persistence;
using FamilyBudget.UnitTests.Factories;

namespace FamilyBudget.UnitTests;
public class BaseRequestTest
{
    protected ApplicationDbContext ApplicationDbContext { get; private set; } = null!;

    [SetUp]
    public void BaseSetup()
    {
        ApplicationDbContext = DbContextFactory.Create();
    }

    [TearDown]
    public virtual async Task BaseTearDown()
    {
        await ApplicationDbContext.DisposeAsync();
    }
}