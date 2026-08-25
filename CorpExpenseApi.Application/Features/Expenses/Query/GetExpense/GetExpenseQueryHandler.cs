using CorpExpenseApi.Application.Interfaces;
using CorpExpenseApi.Domain.Entities;
using CorpExpenseApi.Domain.Exceptions;
using MediatR;

namespace CorpExpenseApi.Application.Features.Expenses.Query.GetExpense;

public class GetExpenseQueryHandler : IRequestHandler<GetExpenseQuery, ExpenseResponse>
{
    private readonly IExpenseRepository _expenseRepository;
    
    public GetExpenseQueryHandler(IExpenseRepository expenseRepository)
    {
        _expenseRepository = expenseRepository;
    }

    public async Task<ExpenseResponse> Handle(GetExpenseQuery request, CancellationToken cancellationToken)
    {
        var expense = await _expenseRepository.GetByIdAsync(request.Id, cancellationToken);

        if (expense == null)
            throw new NotFoundException($"El gasto con ID {request.Id} no existe en la base de datos.");

        return new ExpenseResponse(
            expense.Id,
            expense.Description,
            expense.TotalAmount,
            expense.Currency,
            expense.Status.ToString(),
            expense.Items.Select(i => new ExpenseItemResponse(
                i.Id,
                i.Description,
                i.Amount,
                i.Category
            )).ToList()
        );
    }
}