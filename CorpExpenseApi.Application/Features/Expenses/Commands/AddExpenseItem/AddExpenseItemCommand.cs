using MediatR;

namespace CorpExpenseApi.Application.Features.Expenses.Commands.AddExpenseItem;

public record AddExpenseItemCommand(
        Guid ExpenseId,
        string Description,
        decimal Amount,
        string Category
    ) : IRequest;