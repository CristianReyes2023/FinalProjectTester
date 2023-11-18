using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Dto;
public class BossAndEmployeesDto
{
    public int Id { get; set; }
    public string Name { get; set; }
    public List<EmployeeAndBossDto> Employees { get; set; }
}
