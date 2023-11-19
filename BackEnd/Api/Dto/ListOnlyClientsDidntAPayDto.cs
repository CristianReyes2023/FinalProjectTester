using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Dto;
public class ListOnlyClientsDidntAPayDto
{
    public string NameClient { get; set; }
    public string PhoneNumber { get; set; }
    public double TotalPayment { get; set; }
}
