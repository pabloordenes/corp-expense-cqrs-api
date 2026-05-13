using CorpExpenseApi.Domain.Enums;
using CorpExpenseApi.Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.Text;

namespace CorpExpenseApi.Domain.Entities
{
    public class Expense
    {
        public Guid Id { get; private set; }
        public string Description { get; private set; } = string.Empty;
        public decimal Amount { get; private set; }
        public string Currency { get; private set; } = "USD";
        public DateTime DateIncurred { get; private set; }
        public ExpenseStatus Status { get; private set; }
        public string? ReceiptBlobUrl { get; private set; }

        private Expense() { } // constructor para EF

        // factory method
        public static Expense Create(string description, decimal amount, string currency, DateTime dateIncurred)
        {
            if (amount <= 0)
                throw new DomainException("El monto debe ser mayor a cero");

            if (string.IsNullOrWhiteSpace(description))
                throw new DomainException("La descripción es obligatoria");

            return new Expense
            {
                Id = Guid.NewGuid(),
                Description = description,
                Amount = amount,
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
                
                Status = ExpenseStatus.Submitted;
            }
        }

        // transicion a aprovado
        public void Approve()
        {
            if (Status != ExpenseStatus.Submitted && Status != ExpenseStatus.UnderReview)
                throw new DomainException("El gasto debe estar enviado o en revisión para poder ser aprobado.");

            Status = ExpenseStatus.Approved;
        }

        // comprobante de Azure Blob Storage
        public void AttachReceipt(string blobUrl)
        {
            if (string.IsNullOrWhiteSpace(blobUrl))
                throw new DomainException("La URL del recibo no puede estar vacía.");
            ReceiptBlobUrl = blobUrl;
        }
    }
}
