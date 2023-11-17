using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Entities;
using Domain.Interfaces;
using Persistence.Data;

namespace App.Repositories;
public class ContactClientRepository : GenericRepository<ContactClient> , IContactClient
{
    private readonly GardensContext _context;

    public ContactClientRepository(GardensContext context) : base(context)
    {
        _context = context;
    }
}