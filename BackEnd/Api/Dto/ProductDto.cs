using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Dto;

public class ProductDto
{
    public string Id { get; set; }
    public string Name { get; set; }
    public double Dimensiones { get; set; }
    public string Description { get; set; }
    public int Stock { get; set; }
    public double PriceSale { get; set; }
    public string IdRangerFk { get; set; }
}
