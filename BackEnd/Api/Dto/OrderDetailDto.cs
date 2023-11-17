using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Dto;

public class OrderDetailDto
{
    public int Quantity { get; set; }
    public double UnitPrice { get; set; }
    public int LineNumber { get; set; }
    public int IdOrderFk { get; set; }
    public string IdProductFk { get; set; }
}
