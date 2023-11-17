using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class City : BaseEntityInt
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public int IdStateFk { get; set; }
        public State States  { get; set; }

        public ICollection<OfficeAddress> OfficesAddresses { get; set; }
        public ICollection<ClientAddress> ClientsAddresses { get; set; }
    }
}