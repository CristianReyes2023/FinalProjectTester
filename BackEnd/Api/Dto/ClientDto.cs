using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Dto
{
    public class ClientDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string PhoneNumber { get; set; }
        public string Fax { get; set; }
        public double CreditLimit { get; set; }
        public int IdEmployeeFk { get; set; }
        public int IdContactCliFk { get; set; }
    }
}