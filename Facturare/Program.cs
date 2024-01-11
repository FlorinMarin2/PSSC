using Facturare.Domain;
using Facturare.Domain.Models;
using System;
using System.Collections.Generic;
using static Facturare.Domain.Models.InvoiceChoice;

namespace Facturare
{
    public class Program
    {
        private static IInvoice unvalidatedEvent;
        private static IInvoice generatedEvent;

        static void Main(string[] args)
        {
            Console.Write("Enter client name: ");
            string clientName = Console.ReadLine();

            Console.Write("Enter client email: ");
            string clientEmail = Console.ReadLine();

            ClientDetails clientDetails = new ClientDetails(clientName, clientEmail);

            Console.Write("Enter street address: ");
            string street = Console.ReadLine();

            Console.Write("Enter city: ");
            string city = Console.ReadLine();

            Console.Write("Enter postal code: ");
            string postalCode = Console.ReadLine();

            Console.Write("Enter country: ");
            string country = Console.ReadLine();

            BillingAddress billingAddress = new BillingAddress(street, city, postalCode, country);

            List<InvoiceLine> invoiceLines = new List<InvoiceLine>();

            while (true)
            {
                Console.Write("Enter product code (or 'exit' to finish): ");
                string productCodeInput = Console.ReadLine();

                if (productCodeInput.ToLower() == "exit")
                    break;

                Console.Write("Enter quantity: ");
                int quantity = int.Parse(Console.ReadLine());
                ;

                InvoiceLine invoiceLine = new InvoiceLine(new ProductCode(productCodeInput), new ProductQuantity(quantity), new ProductPrice());
                invoiceLines.Add(invoiceLine);
            }
            Invoice invoice = new Invoice(new InvoiceNumber("INV12345"), DateTime.Now, DateTime.Now.AddDays(30), clientDetails, billingAddress, invoiceLines);
            List<InvoicePaymentDetails> paymentDetails = new List<InvoicePaymentDetails>
            {
                new InvoicePaymentDetails(invoice.Number, invoice.CalculateTotalAmount(), "Credit Card", DateTime.Now)
            };

            ReceivePaymentCommand receivePaymentCommand = new ReceivePaymentCommand(paymentDetails);

            GenerateInvoiceWorkflow generateInvoiceWorkflow = new GenerateInvoiceWorkflow();
            var result = (InvoiceChoice.IInvoice)generateInvoiceWorkflow.Execute(receivePaymentCommand, (invoiceNumber) => true); 

            result.Match(
                whenEmptyInovice: @event =>
                {
                    Console.WriteLine($"Invoice generated successfully for Invoice Number: {invoice.Number}");
                    return unvalidatedEvent;
                },
                whenGeneratedInvoice: @event =>
                {
                    Console.WriteLine($"Failed to generate invoice for Invoice Number: {invoice.Number}");
                    return generatedEvent;
                }
            );
        }
    }
}
