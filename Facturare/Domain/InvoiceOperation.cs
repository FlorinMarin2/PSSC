using Facturare.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using static Facturare.Domain.Models.InvoiceChoice;

namespace Facturare.Domain
{
    public static class InvoiceOperation
    {
        public static IInvoice ValidateInvoice(Func<InvoiceNumber, bool> checkInvoiceExists, UnvalidatedInvoice unvalidatedInvoice)
        {
            List<InvoiceLine> validatedInvoiceLines = new();
            bool isValidList = true;
            string invalidReason = string.Empty;

            foreach (var paymentDetail in unvalidatedInvoice.Payments)
            {
                if (!InvoiceNumber.TryParse(paymentDetail.InvoiceNumber.ToString(), out InvoiceNumber? invoiceNumber) || !checkInvoiceExists(invoiceNumber))
                {
                    invalidReason = $"Invalid invoice number {paymentDetail.InvoiceNumber}!";
                    isValidList = false;
                    break;
                }
               InvoiceLine validInvoiceLine=null;
                validatedInvoiceLines.Add(validInvoiceLine);
            }

            if (isValidList)
            {
                return new ValidatedInvoice(validatedInvoiceLines.AsReadOnly());
            }
            else
            {
                return new UnvalidatedInvoice(unvalidatedInvoice.Payments);
            }
        }

        public static IInvoice CalculateTotalAmount(IInvoice invoice)
        {
            return invoice;
        }

        public static IInvoice GenerateInvoice(IInvoice invoice)
        {
            return invoice;
        }

        public static IInvoice PayInvoice(IInvoice invoice)
        {

            return invoice.Match(
         whenEmptyInovice: nevalidatInvoice => nevalidatInvoice,
         whenGeneratedInvoice: golInvoice =>
      {
             return invoice;
         });

        }
    }
}