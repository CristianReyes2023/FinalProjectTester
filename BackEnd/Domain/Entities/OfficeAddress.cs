using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class OfficeAddress : BaseEntityInt
    {
        public int MainNumber { get; set; }
        public string Letter { get; set; }
        public string Bis { get; set; }
        public string SecLet { get; set; }
        public string Cardinal { get; set; }
        public string SecNum { get; set; }
        public string SecCard { get; set; }
        public string Complet { get; set; }
        public string PosCod { get; set; }

        [Required]
        public int IdCityFk { get; set; }
        public City Cities { get; set; }

        [Required]
        public string IdOfficeFk { get; set; }
        public Office Offices { get; set; }
    }
}