using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Entities;
using Domain.Interfaces;
using Persistence.Data;

namespace App.Repositories;
public class CountryRepository : GenericRepository<Country> , ICountry
{
    private readonly GardensContext _context;

    public CountryRepository(GardensContext context) : base(context)
    {
        _context = context;
    }
}