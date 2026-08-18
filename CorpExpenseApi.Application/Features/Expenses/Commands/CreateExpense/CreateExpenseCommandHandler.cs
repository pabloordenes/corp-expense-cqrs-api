using System.Reflection.Metadata;
using CorpExpenseApi.Application.Interfaces;
using CorpExpenseApi.Domain.Entities;
using MediatR;

namespace CorpExpenseApi.Application.Features.Expenses.Commands.CreateExpense;

public class CreateExpenseCommandHandler : IRequestHandler<CreateExpenseCommand, Guid>
{
    private readonly IExpenseRepository _expenseRepository;

    public CreateExpenseCommandHandler(IExpenseRepository expenseRepository)
    {
        _expenseRepository = expenseRepository;
    }

    public async Task<Guid> Handle(CreateExpenseCommand request, CancellationToken cancellationToken)
    {
        var newExpense = Expense.Create(
            request.Description,
            request.Currency,
            request.DateIncurred
            );

        await _expenseRepository.AddAsync(newExpense, cancellationToken);
        
        return newExpense.Id;
    }
}