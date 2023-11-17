using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class ProductSupplier
    {
        public double SupplierPrice { get; set; }

        [Required]
        public string IdProductFk { get; set; }
        public Product Products { get; set; }
        [Required]
        public int IdSupplierFk { get; set; }
        public Supplier Suppliers { get; set; }
    }
}