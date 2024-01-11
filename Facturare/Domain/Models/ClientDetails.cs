using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Facturare.Domain.Models
{
    public record ClientDetails
    {
        public string Name { get; }
        public string Email { get; }

        public ClientDetails(string name, string email)
        {
            if (string.IsNullOrWhiteSpace(name) || string.IsNullOrWhiteSpace(email))
                throw new ArgumentException("Client name and email cannot be empty.");

            Name = name;
            Email = email;
        }
    }
}
