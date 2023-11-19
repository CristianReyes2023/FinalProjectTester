using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Dto;
public class RangerProductByClientDto
{
    public string NameClient { get; set; }
    public int Order { get; set; }
    public string NameProduct { get; set; }
    public string DescriptionRangerProduct { get; set; }
}
