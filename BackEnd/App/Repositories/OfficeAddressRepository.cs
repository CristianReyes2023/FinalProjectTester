using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Entities;
using Domain.Interfaces;
using Persistence.Data;

namespace App.Repositories;
public class OfficeAddressRepository : GenericRepository<OfficeAddress>, IOfficeAddress
{
    private readonly GardensContext _context;

    public OfficeAddressRepository(GardensContext context) : base(context)
    {
        _context = context;
    }
}