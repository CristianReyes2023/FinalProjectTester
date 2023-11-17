using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Dto;

public class PaymentDto
{
    public string Id { get; set; }
    public DateOnly DatePayment { get; set; }
    public double Total { get; set; }
    public int IdPaymenMetFk { get; set; }
    public int IdClientFk { get; set; }
}
