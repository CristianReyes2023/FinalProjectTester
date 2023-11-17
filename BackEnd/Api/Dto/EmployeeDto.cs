using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Dto;

public class EmployeeDto
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string LastNameOne { get; set; }
    public string LastNameTwo { get; set; }
    public string Extension { get; set; }
    public string Email { get; set; }
    public int IdBoosFk { get; set; }
    public string IdOfficeFk { get; set; }
    public int IdPositionFk { get; set; }
}
