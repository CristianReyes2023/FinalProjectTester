using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Dto;
public class ListCancelledInYear2023Dto
{
    public int IdOrder { get; set; }
    public string StateOrder { get; set; }
    public string Comments { get; set; }
    public DateOnly OrderDate { get; set; }
}