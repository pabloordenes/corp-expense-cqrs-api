using CorpExpenseApi.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace CorpExpenseApi.Infrastructure.Persistence.Configuration
{
    public class ExpenseConfiguration : IEntityTypeConfiguration<Expense>
    {
        public void Configure(EntityTypeBuilder<Expense> builder)
        {
            builder.ToTable("Expenses");
            builder.HasKey(e => e.Id);

            builder.Property(e => e.Description)
                .IsRequired()
                .HasMaxLength(250);

            var navigation = builder.Metadata.FindNavigation(nameof(Expense.Items));
            navigation.SetPropertyAccessMode(PropertyAccessMode.Field);

            builder.HasMany(e => e.Items)
                .WithOne()
                .HasForeignKey("ExpenseId")
                .OnDelete(DeleteBehavior.Cascade);
            
            builder.Ignore(e => e.TotalAmount);

            builder.Property(e => e.Currency)
                .IsRequired()
                .HasMaxLength(3);

            builder.Property(e => e.Status)
                .HasConversion<string>()
                .IsRequired();

            builder.Property(e => e.ReceiptBlobUrl)
                .HasMaxLength(500)
                .IsRequired(false);
        }
    }
}
