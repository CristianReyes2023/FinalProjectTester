using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Dto;

public class OfficeAddressDto
{
    public int Id { get; set; }
    public int MainNumber { get; set; }
    public string Letter { get; set; }
    public string Bis { get; set; }
    public string SecLet { get; set; }
    public string Cardinal { get; set; }
    public string SecNum { get; set; }
    public string SecCard { get; set; }
    public string Complet { get; set; }
    public string PosCod { get; set; }
    public int IdCityFk { get; set; }
    public string IdOfficeFk { get; set; }
}
