using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Facturare.Domain.Models
{
    public record InvoiceLine
    {
        public ProductCode ProductCode { get; }
        public ProductQuantity Quantity { get; }
        public ProductPrice Price { get; }

        public InvoiceLine(ProductCode productCode, ProductQuantity quantity, ProductPrice price)
        {
            ProductCode = productCode ?? throw new ArgumentNullException(nameof(productCode));
            Quantity = quantity ?? throw new ArgumentNullException(nameof(quantity));
            Price = price ?? throw new ArgumentNullException(nameof(price));
        }

        public double CalculateLineTotal() => Quantity.ReturnQuantity() * Price.ReturnPrice();

        public override string ToString() => $"ProductCode={ProductCode} Quantity={Quantity.Value} Price={Price.Value}";
    }
}
