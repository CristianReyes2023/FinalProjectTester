using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Dto;

public class RangerProductDto
{
    public string Id { get; set; }
    public string DescriptionText { get; set; }
    public string DescriptionHtml { get; set; }
    public string Img { get; set; }
}
