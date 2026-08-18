using CorpExpenseApi.Domain.Enums;
using CorpExpenseApi.Domain.Exceptions;

namespace CorpExpenseApi.Domain.Entities;

public class ExpenseItem
{
    public Guid Id { get; private set; }
    public string Description { get; private set; } = string.Empty;
    public decimal Amount { get; private set; }
    public string Category { get; private set; } = string.Empty;
    
    private ExpenseItem() { }

    internal ExpenseItem(string description, decimal amount, string category)
    {
        if (amount <= 0)
            throw new DomainException("El monto de linea debe ser mayor a cero.");

        if (string.IsNullOrWhiteSpace(description))
            throw new DomainException("La descripcion de la linea es obligatoria.");
        
        Id = Guid.NewGuid();
        Description = description;
        Amount = amount;
        Category = category;
    }
}