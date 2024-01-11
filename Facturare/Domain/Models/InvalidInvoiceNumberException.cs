using System;
using System.Runtime.Serialization;

namespace Facturare.Domain.Models
{
    [Serializable]
    internal class InvalidInvoiceNumberException : Exception
    {
        public InvalidInvoiceNumberException()
        {
        }

        public InvalidInvoiceNumberException(string? message) : base(message)
        {
        }

        public InvalidInvoiceNumberException(string? message, Exception? innerException) : base(message, innerException)
        {
        }

        protected InvalidInvoiceNumberException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }

}
