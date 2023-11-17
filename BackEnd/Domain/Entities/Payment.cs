using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Payment : BaseEntityString
    {
        [Required]
        public DateOnly DatePayment { get; set; }
        [Required]
        public double Total { get; set; }

        [Required]
        public int IdPaymenMetFk { get; set; }
        public PaymentMethod PaymentsMethods { get; set; }
        [Required]
        public int IdClientFk { get; set; }
        public Client Clients { get; set; }
    }
}