using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Rol : BaseEntityInt
    {
        public string Name { get; set; } = null!;
        public ICollection<User> Users { get; set; }
        public ICollection<UserRol> UserRols { get; set; }
    }
}

