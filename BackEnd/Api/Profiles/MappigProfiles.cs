using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Api.Dto;
using AutoMapper;
using Domain.Entities;

namespace Api.Profiles;

public class MappigProfiles : Profile
{
    public MappigProfiles()
    {
        CreateMap<Boss, BossDto>().ReverseMap();
        CreateMap<City, CityDto>().ReverseMap();
        CreateMap<ClientAddress, ClientAddressDto>().ReverseMap();
        CreateMap<Client, ClientDto>().ReverseMap();
        CreateMap<ContactClient, ContactClientDto>().ReverseMap();
        CreateMap<Country, CountryDto>().ReverseMap();
        CreateMap<Employee, EmployeeDto>().ReverseMap();
        CreateMap<OfficeAddress, OfficeAddressDto>().ReverseMap();
        CreateMap<Office, OfficeAddress>().ReverseMap();
        CreateMap<OrderDetail, OrderDetailDto>().ReverseMap();
        CreateMap<Order, OrderDto>().ReverseMap();
        CreateMap<Payment, PaymentDto>().ReverseMap();
        CreateMap<PaymentMethod, PaymentMethodDto>().ReverseMap();
        CreateMap<PositionEmployee, PositionEmployeeDto>().ReverseMap();
        CreateMap<Product, ProductDto>().ReverseMap();
        CreateMap<ProductSupplier, ProductSupplierDto>().ReverseMap();
        CreateMap<RangerProduct, RangerProductDto>().ReverseMap();
        CreateMap<State, StateDto>().ReverseMap();
        CreateMap<StateOrder, StateOrderDto>().ReverseMap();
        CreateMap<Supplier, SupplierDto>().ReverseMap();
    }
}
