namespace FamilyBudget.Persistence.Entities.Users;
public sealed class UserEntity
{
    public Guid Id { get; set; }
    public required string Name { get; set; }
}