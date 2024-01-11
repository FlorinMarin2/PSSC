using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Facturare.Domain.Models
{
    public record InvoiceNumber
    {
        private static readonly Regex ValidPattern = new("^[A-Z0-9]{8}$");

        public string Value { get; private set; }

        public InvoiceNumber(string value)
        {
            if (IsValid(value))
            {
                Value = value;
            }
            else
            {
                throw new InvalidInvoiceNumberException("Invalid invoice number format.");
            }
        }

        private static bool IsValid(string stringValue) => ValidPattern.IsMatch(stringValue);

        public override string ToString() => Value;

        public static bool TryParse(string stringValue, out InvoiceNumber? invoiceNumber)
        {
            bool isValid = false;
            invoiceNumber = null;

            if (IsValid(stringValue))
            {
                isValid = true;
                invoiceNumber = new(stringValue);
            }

            return isValid;
        }
    }
}
