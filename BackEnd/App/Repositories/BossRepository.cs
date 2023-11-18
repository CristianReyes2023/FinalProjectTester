using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Entities;
using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using Persistence.Data;

namespace App.Repositories;
public class BossRepository : GenericRepository<Boss>,IBoss
{
    private readonly GardensContext _context;

    public BossRepository(GardensContext context) : base(context)
    {
        _context = context;
    }

    public async Task<Boss> GetEmployeeByBossId(int id)
    {
        return await _context.Bosses.Where( x => x.Id == id)
        .Include(x=>x.Employees)
        .FirstAsync();
    }

}
