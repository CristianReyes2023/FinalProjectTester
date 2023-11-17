using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class PaymentMethod : BaseEntityInt
    {
        [Required]
        public string MethodName { get; set; }

        public ICollection<Payment> Payments { get; set; }
    }
}