using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Dto;
public class ClientsDidPaymentsAndContactClientDto
{
    public string ClientName { get; set; }
    public string EmployeeName { get; set; }
    public string LastNameOne { get; set; }
    public string LastNameTwo { get; set; }
    public double PaymentsTotal { get; set; }
    public string CityOfOffice { get; set; }
}
