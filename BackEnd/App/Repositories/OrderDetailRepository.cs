using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Entities;
using Domain.Interfaces;
using Infrastructure.Repositories;
using Persistence.Data;

namespace App.Repositories;
public class OrderDetailRepository : GenericRepositoryComposedOrderDetail<OrderDetail> , IOrderDetail
{
    private readonly GardensContext _context;

    public OrderDetailRepository(GardensContext context) : base(context)
    {
        _context = context;
    }
}