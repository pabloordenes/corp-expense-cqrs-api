using CorpExpenseApi.Domain.Enums;
using CorpExpenseApi.Domain.Exceptions;
using System.Linq;

namespace CorpExpenseApi.Domain.Entities
{
    public class Expense
    {
        private readonly List<ExpenseItem> _items = new();
        public IReadOnlyCollection<ExpenseItem> Items => _items.AsReadOnly(); 
            
        public Guid Id { get; private set; }
        public string Description { get; private set; } = string.Empty;
        public string Currency { get; private set; } = "USD";
        public decimal TotalAmount => _items.Sum(i => i.Amount);
        public DateTime DateIncurred { get; private set; }
        public ExpenseStatus Status { get; private set; } = 0;
        public string? ReceiptBlobUrl { get; private set; }
        public Guid? ApproverId { get; private set; }
        public string? RejectionReason { get; private set; }

        private Expense() { } // constructor para EF

        // factory method
        public static Expense Create(string description, string currency, DateTime dateIncurred)
        {

            if (string.IsNullOrWhiteSpace(description))
                throw new DomainException("La descripción es obligatoria");

            return new Expense
            {
                Id = Guid.NewGuid(),
                Description = description,
                Currency = currency,
                DateIncurred = dateIncurred,
                Status = ExpenseStatus.Draft
            };
        }

        // solo enviamos si esta en borrador
        public void Submit() 
        {
            if (Status != ExpenseStatus.Draft)
            {
                throw new DomainException($"No se puede enviar un gasto en estado {Status}.");
            }
            
            if (TotalAmount <= 0)
                throw new DomainException("El gasto debe ser mayor a cero.");
            
            Status = ExpenseStatus.Submitted;
        }

        // transicion a aprovado
        public void Approve(Guid approverId)
        {
            if (Status != ExpenseStatus.Submitted && Status != ExpenseStatus.UnderReview)
                throw new DomainException("El gasto debe estar enviado o en revisión para poder ser aprobado.");

            ApproverId = approverId;
            Status = ExpenseStatus.Approved;
            
        }

        public void Reject(string reason)
        {
            if (Status != ExpenseStatus.Submitted && Status != ExpenseStatus.UnderReview)
                throw new DomainException("El gasto debe estar enviado o en revisión.");

            if (string.IsNullOrWhiteSpace(reason))
                throw new DomainException("La razón de rechazo es obligatoria.");
            
            Status = ExpenseStatus.Rejected;
            RejectionReason = reason;   
        }

        // comprobante de Azure Blob Storage
        public void AttachReceipt(string blobUrl)
        {
            if (string.IsNullOrWhiteSpace(blobUrl))
                throw new DomainException("La URL del recibo no puede estar vacía.");
            ReceiptBlobUrl = blobUrl;
        }

        public void AddItem(string description, decimal amount, string category)
        {
            if (Status != ExpenseStatus.Draft)
                throw new DomainException("Solo se pueden agregar lineas a un gasto en estado borrador.");
            
            var newItem = new ExpenseItem(description, amount, category);
            
            _items.Add(newItem);
        }
    }
}
