using System;
using CSharp.Choices;
using Facturare.Domain.Models;



namespace Facturare.Domain.Models
{
    [AsChoice]
    public static partial class InvoiceEvent
    {
        public interface IInvoiceEvent { }

        public record InvoiceGeneratedEvent : IInvoiceEvent
        {
            public InvoiceNumber InvoiceNumber { get; }
            public DateTime GeneratedDate { get; }
           

            internal InvoiceGeneratedEvent(InvoiceNumber invoiceNumber, DateTime generatedDate)
            {
                InvoiceNumber = invoiceNumber;
                GeneratedDate = generatedDate;
                
            }
        }
        public record UnvalidatedInvoiceEvent : IInvoiceEvent
        {
            public string Reason { get; }

            internal UnvalidatedInvoiceEvent(string reason)
            {
                Reason = reason;
            }
        }
    }
}
