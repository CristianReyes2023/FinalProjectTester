using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Dto;
public class ListOrderWithDelayDto
{
    public int IdOrder { get; set; }
    public int IdClient { get; set; }
    public DateOnly DeadLine { get; set; }
    public DateOnly ExpectedDay { get; set; }
}
