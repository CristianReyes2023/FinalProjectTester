using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Product : BaseEntityString
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public double Dimensiones { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public int Stock { get; set; }
        [Required]
        public double PriceSale { get; set; }

        public string IdRangerFk { get; set; }
        public RangerProduct RangersProducts { get; set; }

        public ICollection<OrderDetail> OrdersDetails { get; set; }
        public ICollection<ProductSupplier> ProductsSuppliers { get; set; }
    }
}