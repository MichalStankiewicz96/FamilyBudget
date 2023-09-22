using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FamilyBudget.Persistence.Entities.Transaction;
public sealed class TransactionEntityConfiguration : IEntityTypeConfiguration<TransactionEntity>
{
    public void Configure(EntityTypeBuilder<TransactionEntity> builder)
    {
        builder.HasKey(e => e.Id);

        builder.Property(a => a.Category)
            .IsRequired()
            .HasMaxLength(200);

        //Precision is the number of digits in a number. Scale is the number of digits to the right of the decimal point in a number.
        //For example, the number 123.45 has a precision of 5 and a scale of 2.
        builder.Property(x => x.Amount)
            .HasPrecision(14, 2);

        builder.HasOne(x => x.Budget)
            .WithMany(x => x.Transactions)
            .HasForeignKey(x => x.BudgetId);

        builder.HasIndex(x => x.BudgetId);

        builder.HasIndex(x => x.Category);
    }
}