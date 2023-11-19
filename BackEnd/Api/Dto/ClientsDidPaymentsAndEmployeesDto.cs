using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Dto;
public class ClientsDidPaymentsAndEmployeesDto
{
    public string    ClientId { get; set; }
    public string EmployeeName { get; set; }
    public string LastNameOne { get; set; }
    public string LastNameTwo { get; set; }
    public double PaymentsTotal { get; set; }
    public string IdPayment { get; set; }
}
