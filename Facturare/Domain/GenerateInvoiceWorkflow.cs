using Facturare.Domain.Models;
using System;
using static Facturare.Domain.Models.InvoiceEvent;
using static Facturare.Domain.Models.InvoiceChoice;

namespace Facturare.Domain
{
    public class GenerateInvoiceWorkflow
    {
        public IInvoiceEvent Execute(ReceivePaymentCommand command, Func<InvoiceNumber, bool> checkInvoiceExists)
        {
            UnvalidatedInvoice unvalidatedInvoice1 = new (command.Payments);

            IInvoice invoice = ValidateInvoice(checkInvoiceExists, unvalidatedInvoice1);
            invoice = GenerateInvoice(invoice);

            return (IInvoiceEvent)invoice.Match(


               whenEmptyInovice: unvalidated => (IInvoice)(new UnvalidatedInvoiceEvent("Invalid invoice data") as IInvoiceEvent),
               whenGeneratedInvoice: publicatInvoice => (IInvoice)(new InvoiceGeneratedEvent(publicatInvoice.InvoiceNumber, publicatInvoice.GeneratedDate))

            ) ;
        }

        private IInvoice ValidateInvoice(Func<InvoiceNumber, bool> checkInvoiceExists, UnvalidatedInvoice unvalidatedInvoice)
        {
            var isValid = true; 
            string invalidReason = "";

            foreach (var paymentDetail in unvalidatedInvoice.Payments)
            {
                if (!InvoiceNumber.TryParse(paymentDetail.InvoiceNumber.ToString(), out InvoiceNumber? invoiceNumber) || !checkInvoiceExists(invoiceNumber))
                {
                    isValid = false;
                    invalidReason = $"Invalid invoice number: {paymentDetail.InvoiceNumber}";
                    break;
                }         
            }

            if (!isValid)
            {
                return new ValidatedInvoice(null);
            }
            else
            {
                return new UnvalidatedInvoice(null);
            }
        }

        private IInvoice GenerateInvoice(IInvoice invoice)
        {
            if (invoice is CalculatedInvoice calculatedInvoice)
            {
                return new GeneratedInvoice(null, DateTime.Now);
            }
            else
            {
                throw new InvalidOperationException("Invoice must be calculated before generating.");
            }
        }
    }
}