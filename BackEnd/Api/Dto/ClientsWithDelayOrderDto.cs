using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Dto;
public class ClientsWithDelayOrderDto
{
    public string NameClient { get; set; }
    public int IdOrder { get; set; }
    public int DelayDays { get; set; }
}   
