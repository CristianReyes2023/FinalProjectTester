using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Client : BaseEntityInt
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string PhoneNumber { get; set; }
        [Required]
        public string Fax { get; set; }
        [Required]
        public double CreditLimit { get; set; }

        [Required]
        public int IdEmployeeFk { get; set; }
        public Employee Employees { get; set; }

        [Required]
        public int IdContactCliFk { get; set; }
        public ContactClient ContactsClients { get; set; }

        public ICollection<Payment> Payments { get; set; }
        public ICollection<Order> Orders { get; set; }
        
        public ClientAddress ClientsAddresses { get; set; }
    }
}