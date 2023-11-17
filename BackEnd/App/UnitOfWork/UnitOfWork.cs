using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using App.Repositories;
using Domain.Entities;
using Domain.Interfaces;
using Persistence.Data;

namespace App.UnitOfWork;
public class UnitOfWork : IUnitOfWork, IDisposable
{
    private readonly GardensContext _context;

    public UnitOfWork(GardensContext context)
    {
        _context = context;
    }

    private IBoss _bosses;
    private ICity _cities;
    private IClient _clients;
    private IClientAddress _clientaddresses;
    private IContactClient _contactclients;
    private ICountry _countries;
    private IEmployee _employees;
    private IOffice _offices;
    private IOfficeAddress _officeaddresses;
    private IOrder _orders;
    private IOrderDetail _orderdetails;
    private IPayment _payments;
    private IPaymentMethod _paymentMethods;
    private IPositionEmployee _positionemployees;
    private IProduct _products;
    private IProductSupplier _productsuppliers;
    private IRangerProduct _rangerproducts;
    private IState _states;
    private IStateOrder _stateOrders;
    private ISupplier _supplier;
    private IUser _users;
    private IRol _rols;
    private IRefreshToken _refreshTokens;


    public IBoss Bosses
    {
        get
        {
            if (_bosses == null)
            {
                _bosses = new BossRepository(_context);
            }
            return _bosses;
        }
    }
    public ICity Cities
    {
        get
        {
            if (_cities == null)
            {
                _cities = new CityRepository(_context);
            }
            return _cities;
        }
    }
    public IClient Clients
    {
        get
        {
            if (_clients == null)
            {
                _clients = new ClientRepository(_context);
            }
            return _clients;
        }
    }
    public IClientAddress ClientAddresses
    {
        get
        {
            if (_clientaddresses == null)
            {
                _clientaddresses = new ClientAddressRepository(_context);
            }
            return _clientaddresses;
        }
    }
    public IContactClient ContactsClients
    {
        get
        {
            if (_contactclients == null)
            {
                _contactclients = new ContactClientRepository(_context);
            }
            return _contactclients;
        }
    }
    public ICountry Countries
    {
        get
        {
            if (_countries == null)
            {
                _countries = new CountryRepository(_context);
            }
            return _countries;
        }
    }
    public IEmployee Employees
    {
        get
        {
            if (_employees == null)
            {
                _employees = new EmployeeRepository(_context);
            }
            return _employees;
        }
    }
    public IOffice Offices
    {
        get
        {
            if (_offices == null)
            {
                _offices = new OfficeRepository(_context);
            }
            return _offices;
        }
    }
    public IOfficeAddress OfficeAddresses
    {
        get
        {
            if (_officeaddresses == null)
            {
                _officeaddresses = new OfficeAddressRepository(_context);
            }
            return _officeaddresses;
        }
    }
    public IOrder Orders
    {
        get
        {
            if (_orders == null)
            {
                _orders = new OrderRepository(_context);
            }
            return _orders;
        }
    }
    public IOrderDetail OrderDetails
    {
        get
        {
            if (_orderdetails == null)
            {
                _orderdetails = new OrderDetailRepository(_context);
            }
            return _orderdetails;
        }
    }
    public IPayment Payments
    {
        get
        {
            if (_payments == null)
            {
                _payments = new PaymentRepository(_context);
            }
            return _payments;
        }
    }
    public IPaymentMethod PaymentMethods
    {
        get
        {
            if (_paymentMethods == null)
            {
                _paymentMethods = new PaymentMethodRepository(_context);
            }
            return _paymentMethods;
        }
    }
    public IPositionEmployee PositionEmployees
    {
        get
        {
            if (_positionemployees == null)
            {
                _positionemployees = new PositionEmployeeRepository(_context);
            }
            return _positionemployees;
        }
    }
    public IProduct Products
    {
        get
        {
            if (_products == null)
            {
                _products = new ProductRepository(_context);
            }
            return _products;
        }
    }
    public IProductSupplier ProductSuppliers
    {
        get
        {
            if (_productsuppliers == null)
            {
                _productsuppliers = new ProductSupplierRepository(_context);
            }
            return _productsuppliers;
        }
    }
    public IRangerProduct RangesProducts
    {
        get
        {
            if (_rangerproducts == null)
            {
                _rangerproducts = new RangerProductRepository(_context);
            }
            return _rangerproducts;
        }
    }
    public IState States
    {
        get
        {
            if (_states == null)
            {
                _states = new StateRepository(_context);
            }
            return _states;
        }
    }
    public IStateOrder StateOrders
    {
        get
        {
            if (_stateOrders == null)
            {
                _stateOrders = new StateOrderRepository(_context);
            }
            return _stateOrders;
        }
    }
    public ISupplier Suppliers
    {
        get
        {
            if (_supplier == null)
            {
                _supplier = new SupplierRepository(_context);
            }
            return _supplier;
        }
    }
    
    public IUser Users
    {
        get
        {
            if (_users == null)
            {
                _users = new UserRepository (_context); // Remember putting the base in the repository of this entity
            }
            return _users;
        }
    }

    public IRol Rols
    {
        get
        {
            if (_rols == null)
            {
                _rols = new RolRepository (_context); // Remember putting the base in the repository of this entity
            }
            return _rols;
        }
    }
    

    public IRefreshToken RefreshTokens
    {
        get
        {
            if (_refreshTokens == null)
            {
                _refreshTokens = new RefreshTokenRepository (_context); // Remember putting the base in the repository of this entity
            }
            return _refreshTokens;
        }
    }
    public Task<int> SaveAsync()
    {
        return _context.SaveChangesAsync();
    }

    public void Dispose()
    {
        _context.Dispose();
    }
}
