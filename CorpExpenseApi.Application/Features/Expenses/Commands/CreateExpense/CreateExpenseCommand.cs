using MediatR;

namespace CorpExpenseApi.Application.Features.Expenses.Commands.CreateExpense;

public record CreateExpenseCommand(
    string Description,
    string Currency,
    DateTime DateIncurred
    ) : IRequest<Guid>;