using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Dto;
public class ListOfPayPalPaymentDto
{
    public string MethodPayment { get; set; }
    public DateOnly DatePayment { get; set; }
}
