using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class RangerProduct : BaseEntityString
    {
        [Required]
        public string DescriptionText { get; set; }
        [Required]
        public string DescriptionHtml { get; set; }
        [Required]
        public string Img { get; set; }

        public ICollection<Product> Products { get; set; }
    }
}