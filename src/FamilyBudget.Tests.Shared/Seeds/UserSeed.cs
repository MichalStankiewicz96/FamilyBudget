using FamilyBudget.Persistence;
using FamilyBudget.Persistence.Entities.Users;

namespace FamilyBudget.Tests.Shared.Seeds;
public sealed class UserSeed : BaseSeed
{
    public UserSeed(ApplicationDbContext context) : base(context) { }

    public override async Task SeedAsync()
    {
        var firstUser = CreateFirsUser();
        Context.Users.Add(firstUser);
        await Context.SaveChangesAsync();
    }

    private UserEntity CreateFirsUser()
    {
        return new UserEntity
        {
            Id = Guid.NewGuid(),
            Name = "FirstUser"
        };
    }
}