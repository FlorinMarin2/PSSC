using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Facturare.Domain.Models
{
    public record Invoice
    {
        private static readonly Regex ValidPatternNumber = new("^[A-Z0-9]{8}$");

        public InvoiceNumber Number { get; set; }
        public DateTime Date { get; }
        public DateTime DueDate { get; }
        public ClientDetails Client { get; }
        public List<InvoiceLine> InvoiceLines { get; }
        public BillingAddress BillingAddress { get; }

        public Invoice(InvoiceNumber number, DateTime date, DateTime dueDate, ClientDetails client, BillingAddress billingAddress, List<InvoiceLine> invoiceLines)
        {
            if (!IsValidNumber(number.Value))
                throw new ArgumentException("Invalid invoice number.");

            Number = number;
            Date = date;
            DueDate = dueDate;
            Client = client;
            BillingAddress = billingAddress;
            InvoiceLines = invoiceLines;
        }

        public double CalculateTotalAmount()
        {
            double total = 0;
            foreach (var line in InvoiceLines)
            {
                total += line.CalculateLineTotal();
            }
            return total;
        }

        public override string ToString()
        {
            return $"Number={Number} Date={Date.ToShortDateString()} DueDate={DueDate.ToShortDateString()} Client={Client} BillingAddress={BillingAddress}";
        }

        private static bool IsValidNumber(string numberValue) => ValidPatternNumber.IsMatch(numberValue);

        public static bool TryParseNumber(string stringValue, out InvoiceNumber? number)
        {
            bool isValid = false;
            number = null;
            if (IsValidNumber(stringValue))
            {
                isValid = true;
                number = new(stringValue);
            }
            return isValid;
        }
    }
}
