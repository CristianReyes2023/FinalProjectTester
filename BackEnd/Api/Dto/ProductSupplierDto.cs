using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Dto;

public class ProductSupplierDto
{
    public double SupplierPrice { get; set; }
    public string IdProductFk { get; set; }
    public int IdSupplierFk { get; set; }
}
