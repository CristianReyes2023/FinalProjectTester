using Domain.Entities;
using Domain.Interfaces;
using Persistence;
using Persistence.Data;

namespace App.Repositories;

public class RolRepository : GenericRepository<Rol>, IRol
{
    private readonly GardensContext _context;

    public RolRepository(GardensContext context) : base(context)
    {
       _context = context;
    }
}
