using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class OrderDetail
    {
        [Required]
        public int Quantity { get; set; }
        [Required]
        public double UnitPrice { get; set; }
        [Required]
        public int LineNumber { get; set; }

        [Required]
        public int IdOrderFk { get; set; }
        public Order Orders { get; set; }
        [Required]
        public string IdProductFk { get; set; }
        public Product Products { get; set; }
    }
}