using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Entities;
using Domain.Interfaces;
using Persistence.Data;

namespace App.Repositories;
public class ProductRepository : GenericRepositoryString<Product>,IProduct
{
    private readonly GardensContext _context;

    public ProductRepository(GardensContext context) : base(context)
    {
        _context = context;
    }
}