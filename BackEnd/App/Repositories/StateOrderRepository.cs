using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Entities;
using Domain.Interfaces;
using Persistence.Data;

namespace App.Repositories;
public class StateOrderRepository : GenericRepository<StateOrder> ,IStateOrder
{
    private readonly GardensContext _context;

    public StateOrderRepository(GardensContext context) : base(context)
    {
        _context = context;
    }
}