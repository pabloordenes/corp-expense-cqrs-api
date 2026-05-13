using CorpExpenseApi.Domain.Entities;

namespace CorpExpenseApi.Application.Interfaces
{
    public interface IExpenseRepository
    {
        Task<Expense?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
        Task AddAsync(Expense expense, CancellationToken cancellationToken = default);
        Task UpdateAsync(Expense expense, CancellationToken cancellationToken = default);
    }
}
