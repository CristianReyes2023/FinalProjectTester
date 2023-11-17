using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Employee : BaseEntityInt
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string LastNameOne { get; set; }
        [Required]
        public string LastNameTwo { get; set; }
        [Required]
        public string Extension { get; set; }
        [Required]
        public string Email { get; set; }

        [Required]
        public int IdBoosFk { get; set; }
        public Boss Bosses  { get; set; }
        [Required]
        public string IdOfficeFk { get; set; }
        public Office Offices  { get; set; }
        [Required]
        public int IdPositionFk { get; set; }
        public PositionEmployee PositionsEmployees  { get; set; }

        public ICollection<Client> Clients { get; set; }
    }
}