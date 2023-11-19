using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Dto;
public class EmployeesAndBossDto
{   
    public string EmployeeName { get; set; }
    public string BossName { get; set; }
    public int IdBoosFk { get; set; }
    
}
