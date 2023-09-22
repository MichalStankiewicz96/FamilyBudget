using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FamilyBudget.Persistence.Entities.BudgetMembers;
public sealed class BudgetMemberEntityConfiguration : IEntityTypeConfiguration<BudgetMemberEntity>
{
    public void Configure(EntityTypeBuilder<BudgetMemberEntity> builder)
    {
        builder.HasKey(x => new { x.BudgetId, x.UserId });

        builder.HasOne(x => x.Budget)
            .WithMany(x => x.BudgetMembers)
            .HasForeignKey(x => x.BudgetId);

        builder.HasOne(x => x.User)
            .WithMany()
            .HasForeignKey(x => x.UserId);

        builder.HasIndex(x => x.BudgetId);

        builder.HasIndex(x => x.UserId);
    }
}
