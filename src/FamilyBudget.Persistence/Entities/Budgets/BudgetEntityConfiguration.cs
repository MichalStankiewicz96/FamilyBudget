using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FamilyBudget.Persistence.Entities.Budgets;
public sealed class BudgetEntityConfiguration : IEntityTypeConfiguration<BudgetEntity>
{
    public void Configure(EntityTypeBuilder<BudgetEntity> builder)
    {
        builder.HasKey(e => e.Id);

        builder.Property(a => a.Description)
            .IsRequired()
            .HasMaxLength(200);
    }
}

