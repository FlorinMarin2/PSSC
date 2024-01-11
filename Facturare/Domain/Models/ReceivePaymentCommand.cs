using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Facturare.Domain.Models
{
    public record ReceivePaymentCommand
    {
        public IReadOnlyCollection<InvoicePaymentDetails> Payments { get; }

        public ReceivePaymentCommand(IReadOnlyCollection<InvoicePaymentDetails> payments)
        {
            Payments = payments;
        }
    }
    public record InvoicePaymentDetails(InvoiceNumber InvoiceNumber, double Amount, string Method, DateTime PaymentDate);
    
}
namespace System.Runtime.CompilerServices
{
    public class IsExternalInit { }
}