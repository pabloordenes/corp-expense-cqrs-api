using CorpExpenseApi.Application.Features.Expenses.Commands.AddExpenseItem;
using CorpExpenseApi.Application.Features.Expenses.Commands.CreateExpense;
using CorpExpenseApi.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CorpExpenseApi.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ExpenseController : ControllerBase
{
    private readonly IMediator _mediator;

    public ExpenseController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    public async Task<IActionResult> CreateExpense([FromBody] CreateExpenseCommand command,
        CancellationToken cancellationToken)
    {
        var expenseId = await _mediator.Send(command, cancellationToken);
        return Ok(new { Id = expenseId });
    }

    [HttpPost("{id}/items")]
    public async Task<IActionResult> AddItem(Guid id, [FromBody] AddItemRequest request,
        CancellationToken cancellationToken)
    {
        var command = new AddExpenseItemCommand(id, request.Description, request.Amount, request.Category);
        
        await _mediator.Send(command, cancellationToken);
        
        return Ok(new {Message = "Línea añadida con éxito."});
    }

    public record AddItemRequest(string Description, decimal Amount, string Category);
}


