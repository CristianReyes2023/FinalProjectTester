using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Dto;

public class OrderDto
{
    public int Id { get; set; }
    public DateOnly OrderDate { get; set; }
    public DateOnly DeadlineDate { get; set; }
    public DateOnly ExpectedDate { get; set; }
    public string Comments { get; set; }
    public int IdClientFk { get; set; }
    public int IdStateOrderFk { get; set; }
}
