using System;
using System.Runtime.Serialization;

namespace Facturare.Domain.Models
{
    [Serializable]
    internal class InvalidBillingAddressException : Exception
    {
        public InvalidBillingAddressException()
        {
        }

        public InvalidBillingAddressException(string? message) : base(message)
        {
        }

        public InvalidBillingAddressException(string? message, Exception? innerException) : base(message, innerException)
        {
        }

        protected InvalidBillingAddressException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
