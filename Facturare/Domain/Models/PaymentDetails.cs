using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Facturare.Domain.Models
{
    public record PaymentDetails
    {
        public string Method { get; private set; }
        public DateTime PaymentDate { get; private set; }
        public double Amount { get; private set; }

        public PaymentDetails(string method, DateTime paymentDate, double amount)
        {
            if (string.IsNullOrWhiteSpace(method))
            {
                throw new ArgumentException("Payment method is required.");
            }

            if (paymentDate < DateTime.UtcNow)
            {
                throw new ArgumentException("Payment date cannot be in the past.");
            }

            if (amount <= 0)
            {
                throw new ArgumentException("Amount must be greater than zero.");
            }

            Method = method;
            PaymentDate = paymentDate;
            Amount = amount;
        }

        public override string ToString() => $"Method: {Method}, Date: {PaymentDate.ToShortDateString()}, Amount: {Amount}";
    }
}
