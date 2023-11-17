using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Office : BaseEntityString
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string Phone { get; set; }

        public OfficeAddress OfficesAddresses { get; set; }

        public ICollection<Employee> Employees { get; set; }
    }
}