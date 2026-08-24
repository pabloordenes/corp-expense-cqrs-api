using CorpExpenseApi.Domain.Entities;
using MediatR;

namespace CorpExpenseApi.Application.Features.Expenses.Query.GetExpense;

public record ExpenseResponse(
    Guid Id,
    string Description,
    decimal TotalAmount,
    string Currency,
    string Status,
    IEnumerable<ExpenseItemResponse> Items);

public record ExpenseItemResponse(
    Guid Id,
    string Description,
    decimal Amount,
    string Category
);

public record GetExpenseQuery(
    Guid Id) :  IRequest<ExpenseResponse>;