using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Facturare.Domain.Models
{
    public record InvoiceDueDate
    {
        public DateTime Value { get; private set; }

        public InvoiceDueDate(DateTime value)
        {
            if (value < DateTime.UtcNow)
            {
                throw new ArgumentException("Due date cannot be in the past.");
            }

            Value = value;
        }

        public override string ToString() => Value.ToShortDateString();
    }
}
