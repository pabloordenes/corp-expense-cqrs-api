using CorpExpenseApi.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CorpExpenseApi.Infrastructure.Persistence.Configuration;

public class ExpenseItemConfiguration : IEntityTypeConfiguration<ExpenseItem>
{
    public void Configure(EntityTypeBuilder<ExpenseItem> builder)
    {
        builder.ToTable("ExpenseItems");
        
        builder.HasKey(ei => ei.Id);

        builder.Property(ei => ei.Amount)
            .HasPrecision(18, 2)
            .IsRequired();

        builder.Property(ei => ei.Description)
            .IsRequired()
            .HasMaxLength(250);
        
        builder.Property(ei => ei.Category)
            .IsRequired()
            .HasMaxLength(100);
    }
}