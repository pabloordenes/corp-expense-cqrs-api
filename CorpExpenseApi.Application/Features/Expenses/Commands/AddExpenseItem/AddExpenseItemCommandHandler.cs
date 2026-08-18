using CorpExpenseApi.Application.Interfaces;
using CorpExpenseApi.Domain.Exceptions;
using MediatR;

namespace CorpExpenseApi.Application.Features.Expenses.Commands.AddExpenseItem;

public class AddExpenseItemCommandHandler : IRequestHandler<AddExpenseItemCommand>
{
    private readonly IExpenseRepository _expenseRepository;

    public AddExpenseItemCommandHandler(IExpenseRepository expenseRepository)
    {
        _expenseRepository = expenseRepository;
    }

    public async Task Handle(AddExpenseItemCommand request, CancellationToken cancellationToken)
    {
        var expense = await _expenseRepository.GetByIdAsync(request.ExpenseId);

        if (expense == null)
            throw new DomainException($"No se encontró el gasto con ID {request.ExpenseId}");
        
        expense.AddItem(request.Description, request.Amount, request.Category);
        
        await _expenseRepository.UpdateAsync(expense);
    }
}