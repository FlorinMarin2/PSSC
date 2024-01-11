using CSharp.Choices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Facturare.Domain.Models
{
    [AsChoice]
    public static partial class InvoiceChoice
    {
        public interface IInvoice
        {
            object InvoiceNumber { get; }
        }

        public record UnvalidatedInvoice : IInvoice
        {
            public IReadOnlyCollection<InvoicePaymentDetails> Payments { get; }

            public object InvoiceNumber => throw new NotImplementedException();

            public UnvalidatedInvoice(IReadOnlyCollection<InvoicePaymentDetails> payments)
            {
                Payments = payments;
            }
        }

        public record ValidatedInvoice : IInvoice
        {
            public IReadOnlyCollection<InvoiceLine> InvoiceLines { get; }

            public object InvoiceNumber => throw new NotImplementedException();

            public ValidatedInvoice(IReadOnlyCollection<InvoiceLine> invoiceLines)
            {
                InvoiceLines = invoiceLines;
            }
        }

        public record CalculatedInvoice : IInvoice
        {
            public IReadOnlyCollection<InvoiceLine> InvoiceLines { get; }
            public double TotalAmount { get; }

            public object InvoiceNumber => throw new NotImplementedException();

            public CalculatedInvoice(IReadOnlyCollection<InvoiceLine> invoiceLines, double totalAmount)
            {
                InvoiceLines = invoiceLines;
                TotalAmount = totalAmount;
            }
        }

        public record GeneratedInvoice : IInvoice
        {
            public InvoiceNumber InvoiceNumber { get; }
            public DateTime GeneratedDate { get; }

            object IInvoice.InvoiceNumber => throw new NotImplementedException();

            public GeneratedInvoice(InvoiceNumber invoiceNumber, DateTime generatedDate)
            {
                InvoiceNumber = invoiceNumber;
                GeneratedDate = generatedDate;
            }
        }

        public static IInvoice Match(this IInvoice self, System.Func<UnvalidatedInvoice, IInvoice> whenEmptyInovice, System.Func<GeneratedInvoice, IInvoice>whenGeneratedInvoice)
        {
            switch ((self))
            {
                case UnvalidatedInvoice unvalidatednvoicet:
                    return whenEmptyInovice(unvalidatednvoicet);
                case GeneratedInvoice generatednvoicet:
                    return whenGeneratedInvoice(generatednvoicet);
                default:
                    throw new System.NotSupportedException("This switch statement should be exhaustive");
            }
        }
    }
}
