using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Persistence.Data;
public class GardensContext : DbContext
{
    public GardensContext(DbContextOptions options) : base(options)
    {
    }

    public DbSet<Boss> Bosses { get; set; }
    public DbSet<City> Cities { get; set; }
    public DbSet<Client> Clients { get; set; }
    public DbSet<ClientAddress> ClientsAddresses { get; set; }
    public DbSet<ContactClient> ContactsClients { get; set; }
    public DbSet<Country> Countries { get; set; }
    public DbSet<Employee> Employees { get; set; }
    public DbSet<Office> Offices { get; set; }
    public DbSet<OfficeAddress> OfficesAddresses { get; set; }
    public DbSet<Order> Orders { get; set; }
    public DbSet<OrderDetail> OrdersDetails { get; set; }
    public DbSet<Payment> Payments { get; set; }
    public DbSet<PaymentMethod> PaymentsMethods { get; set; }
    public DbSet<PositionEmployee> PositionsEmployees { get; set; }
    public DbSet<Product> Products { get; set; }
    public DbSet<ProductSupplier> ProductsSuppliers { get; set; }
    public DbSet<RangerProduct> RangersProducts { get; set; }
    public DbSet<State> States { get; set; }
    public DbSet<StateOrder> StatesOrders { get; set; }
    public DbSet<Supplier> Suppliers { get; set; }
    public DbSet<User> Users { get; set; }
    public DbSet<Rol> Rols { get; set; }
    public DbSet<RefreshToken> RefreshTokens { get; set; }
    public DbSet<UserRol> UsersRols { get; set; }



    protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
}
