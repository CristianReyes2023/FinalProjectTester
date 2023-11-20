using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Domain.Interfaces;
public interface IUnitOfWork
{
    IBoss Bosses {get;}
    ICity Cities {get;}
    IClient Clients {get;}
    IClientAddress ClientAddresses {get;}
    ICountry Countries {get;}
    IContactClient ContactsClients {get;}
    IEmployee Employees {get;}
    IOffice Offices {get;}
    IOfficeAddress OfficeAddresses {get;}
    IOrder Orders {get;}
    IOrderDetail OrderDetails {get;}
    IPayment Payments {get;}
    IPaymentMethod PaymentMethods {get;}
    IPositionEmployee PositionEmployees {get;}
    IProduct Products {get;}
    IProductSupplier ProductSuppliers {get;}
    IRangerProduct RangesProducts {get;}
    IState States {get;}
    IStateOrder StateOrders {get;}
    ISupplier Suppliers {get;}
    IRefreshToken RefreshTokens {get;}
    IRol Roles {get;}
    IUser Users {get;}
    Task<int> SaveAsync();
}
