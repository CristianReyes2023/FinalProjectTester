using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class StateOrder : BaseEntityInt
    {
        [Required]
        public string Name { get; set; }

        public ICollection<Order> Orders { get; set; }
    }
}