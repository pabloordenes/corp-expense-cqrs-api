using CorpExpenseApi.Application.Interfaces;
using CorpExpenseApi.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;

namespace CorpExpenseApi.Infrastructure.Persistence.Repositories
{
    public class ExpenseRepository : IExpenseRepository
    {
        private readonly AppDbContext _context;

        public ExpenseRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Expense?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
        {
            return await _context.Expenses
                .Include(e => e.Items)
                .FirstOrDefaultAsync(e => e.Id == id, cancellationToken);
        }

        public async Task AddAsync(Expense expense, CancellationToken cancellationToken = default)
        {
            await _context.Expenses.AddAsync(expense, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
        }

        public async Task UpdateAsync(Expense expense, CancellationToken cancellationToken = default)
        {
            _context.Expenses.Update(expense);
            await _context.SaveChangesAsync(cancellationToken);
        }
    }
}
