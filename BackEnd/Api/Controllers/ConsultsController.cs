using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Threading.Tasks;
using Api.Dto;
using AutoMapper;
using Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Persistence.Data;

namespace Api.Controllers;
public class ConsultsController : BaseController
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly GardensContext _context;

    public ConsultsController(IUnitOfWork unitOfWork, IMapper mapper, GardensContext context)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _context = context;
    }

    //Consulta 1: Devuelve un listado con el código de oficina y la ciudad donde hay oficinas.

    [HttpGet("OfficeAndCity_1")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<IEnumerable<OfficeAddressCityDto>>> GetOfficeAndCity()
    {
        var results = await (from toffice in _context.Offices
                            join tofficeaddress in _context.OfficesAddresses on toffice.Id equals tofficeaddress.IdOfficeFk
                            join tcity in _context.Cities on tofficeaddress.IdCityFk equals tcity.Id
                            select new OfficeAddressCityDto
                            {
                                IdOffice = toffice.Id,
                                NameCity = tcity.Name
                            }).ToListAsync();

        return Ok(results);
    }

    //Consulta 2: Devuelve un listado con la ciudad y el teléfono de las oficinas de España.    

    [HttpGet("CityAndOfficePhoneSpain_2")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<IEnumerable<CityAndOfficePhoneCountryDto>>> CityAndOfficePhoneCountry()
    {
        var results = await (from toffice in _context.Offices
                            join tofficeaddress in _context.OfficesAddresses on toffice.Id equals tofficeaddress.IdOfficeFk
                            join tcity in _context.Cities on tofficeaddress.IdCityFk equals tcity.Id
                            join tstate in _context.States on tcity.IdStateFk equals tstate.Id
                            join tcountry in _context.Countries on tstate.IdCountryFk equals tcountry.Id
                            where tcountry.Name == "Spain"
                            select new CityAndOfficePhoneCountryDto
                            {
                                PhoneNumber = toffice.Phone,
                                NameCity = tcity.Name,
                                NameCountry = tcountry.Name
                            }).ToListAsync();

        return Ok(results);
    }

    //Consulta 3: Devuelve un listado con el nombre, apellidos y email de los empleados cuyo jefe tiene un código de jefe igual a 7.

    [HttpGet("GetEmployeeByBossId_3")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<BossAndEmployeesDto>> GetEmployeeByBossId()
    {
        var result = await _unitOfWork.Bosses.GetEmployeeByBossId(1);
        if (result == null)
        {
            return NotFound();
        }
        return _mapper.Map<BossAndEmployeesDto>(result);
    }

    //Consulta 4: Devuelve el nombre del puesto, nombre, apellidos y email del jefe de la empresa.

    [HttpGet("GetPositionNameEmailOfManager_4")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<IEnumerable<PositionNameEmailOfManagerDto>>> GetPositionNameEmailOfManager()
    {
        var results = await (from temployee in _context.Employees
                             join tboss in _context.Bosses on temployee.IdBoosFk equals tboss.Id
                             join tposition in _context.PositionsEmployees on temployee.IdPositionFk equals tposition.Id
                             where tposition.Name == "Manager"
                             select new PositionNameEmailOfManagerDto
                             {
                                 Position = tposition.Name,
                                 Name = temployee.Name,
                                 LastNameOne = temployee.LastNameOne,
                                 LastNameTwo = temployee.LastNameTwo
                             }).ToListAsync();

        return Ok(results);
    }

    //Consulta 5: Devuelve un listado con el nombre, apellidos y puesto de aquellos empleados que no sean representantes de ventas.

    [HttpGet("GetEmployeeAreNotSalesAssociate_5")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<IEnumerable<EmployeeAreNotSalesAssociateDto>>> GetEmployeeAreNotSalesAssociate()
    {
        var position = "Sales Associate";
        var results = await (from temployee in _context.Employees
                             join tboss in _context.Bosses on temployee.IdBoosFk equals tboss.Id
                             join tposition in _context.PositionsEmployees on temployee.IdPositionFk equals tposition.Id
                             where tposition.Name.Trim().ToLower() != position.Trim().ToLower()
                             select new EmployeeAreNotSalesAssociateDto
                             {
                                 Name = temployee.Name,
                                 LastNameOne = temployee.LastNameOne,
                                 LastNameTwo = temployee.LastNameTwo,
                                 Position = tposition.Name
                             }).ToListAsync();
        return Ok(results);
    }

    //Consulta 6: Devuelve un listado con el nombre de los todos los clientes españoles.

    [HttpGet("GetClientsBySpain_6")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<IEnumerable<ClientsByCountryDto>>> GetClientsByCountry()
    {
        var results = await (from tclientaddress in _context.ClientsAddresses
                             join tclient in _context.Clients on tclientaddress.IdClientFk equals tclient.Id
                             join tcity in _context.Cities on tclientaddress.IdCityFk equals tcity.Id
                             join tstate in _context.States on tcity.IdStateFk equals tstate.Id
                             join tcountry in _context.Countries on tstate.IdCountryFk equals tcountry.Id
                             where tcountry.Name.Trim().ToLower() == "Spain"
                             select new ClientsByCountryDto
                             {
                                 NameClient = tclient.Name,
                                 NameCountry = tcountry.Name
                             }).ToListAsync();
        return Ok(results);
    }

    //Consulta 7: Devuelve un listado con los distintos estados por los que puede pasar un pedido.

    [HttpGet("GetListOfStateOrder_7")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<IEnumerable<ListOfStateOrderDto>>> GetListOfStateOrder()
    {
        var results = await (from tstateorder in _context.StatesOrders
                             select new ListOfStateOrderDto
                             {
                                 NameStateOrder = tstateorder.Name
                             }).ToListAsync();
        return Ok(results);
    }

    //Consulta 8: Devuelve un listado con el código de cliente de aquellos clientes que realizaron algún pago en 2008. Tenga en cuenta que deberá eliminar aquellos códigos de cliente que aparezcan repetidos. Resuelva la consulta:
    // Utilizando la función YEAR de MySQL.

    [HttpGet("GetCodeOfClientByDate(YearMySQL_8")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<IEnumerable<CodeOfClientByDateYearDto>>> GetCodeOfClientByDate(int year)
        {
            var results = await (from tpayment in _context.Payments
                                where tpayment.DatePayment.Year == year
                                group tpayment by tpayment.IdClientFk into paymentGroup
                                select new CodeOfClientByDateYearDto
                                {
                                    CodePayment = paymentGroup.First().Id,
                                    DatePayment = paymentGroup.First().DatePayment.Year.ToString(),
                                    CodeClient = paymentGroup.Key
                                })
                                .ToListAsync();
            return Ok(results);
        }

    // Utilizando la función DATE_FORMAT de MySQL.

[HttpGet("GetCodeOfClientByDate(DateFormatMySQL_8")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<IEnumerable<CodeOfClientByDateYearDto>>> GetCodeOfClientByDateFromat(int year)
        {
            var results = await (from tpayment in _context.Payments
                                where tpayment.DatePayment.Year == year
                                group tpayment by tpayment.IdClientFk into paymentGroup
                                select new CodeOfClientByDateYearDto
                                {
                                    CodePayment = paymentGroup.First().Id,
                                    DatePayment = paymentGroup.First().DatePayment.ToString("dddd MMMM yyyy"),
                                    CodeClient = paymentGroup.Key
                                })
                                .ToListAsync();
            return Ok(results);
        }
    // Sin utilizar ninguna de las funciones anteriores.

        [HttpGet("GetCodeOfClientByNormalDate_8")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<IEnumerable<CodeOfClientByDateYearDto>>> GetCodeOfClientByNormalDate(int year)
        {
            var results = await (from tpayment in _context.Payments
                                where tpayment.DatePayment.Year == year
                                group tpayment by tpayment.IdClientFk into paymentGroup
                                select new CodeOfClientByDateYearDto
                                {
                                    CodePayment = paymentGroup.First().Id,
                                    DatePayment = paymentGroup.First().DatePayment.ToString(),
                                    CodeClient = paymentGroup.Key
                                })
                                .ToListAsync();
            return Ok(results);
        }

    //Consulta 9: Devuelve un listado con el código de pedido, código de cliente, fecha esperada y fecha de entrega de los pedidos que no han sido entregados a tiempo.

        [HttpGet("GetListOrderWithDelay_9")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<IEnumerable<ListOrderWithDelayDto>>> GetListOrderWithDelay()
        {
            var results = await (from torder in _context.Orders
                                join tclient in _context.Clients on torder.IdClientFk equals tclient.Id 
                                where torder.ExpectedDate.Day > torder.DeadlineDate.Day
                                select new ListOrderWithDelayDto
                                {
                                    IdOrder = torder.Id,
                                    IdClient = tclient.Id,
                                    DeadLine = torder.DeadlineDate,
                                    ExpectedDay = torder.ExpectedDate
                                })
                                .ToListAsync();
            return Ok(results);
        }


    //Consulta 10: Devuelve un listado con el código de pedido, código de cliente, fecha esperada y fecha de entrega de los pedidos cuya fecha de entrega ha sido al menos dos días antes de la fecha esperada.

        [HttpGet("GetListOrderCompletedOnTime_10")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<IEnumerable<ListOrderWithDelayDto>>> GetListOrderCompletedOnTime()
        {   
            
            var results = await (from torder in _context.Orders
                                join tclient in _context.Clients on torder.IdClientFk equals tclient.Id 
                                where (torder.DeadlineDate.DayNumber - torder.ExpectedDate.DayNumber) > 2
                                select new ListOrderWithDelayDto
                                {
                                    IdOrder = torder.Id,
                                    IdClient = tclient.Id,
                                    DeadLine = torder.DeadlineDate,
                                    ExpectedDay = torder.ExpectedDate
                                })
                                .ToListAsync();
            return Ok(results);
        }

        //Consulta 11: Devuelve un listado de todos los pedidos que fueron rechazados en 2023.

        [HttpGet("GetListCancelledInYear(2023)_11")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<IEnumerable<ListCancelledInYear2023Dto>>> GetListCancelledInYear()
        {
            var results = await (from torder in _context.Orders
                                join tstateorder in _context.StatesOrders on torder.IdStateOrderFk equals tstateorder.Id 
                                where tstateorder.Id == 5
                                select new ListCancelledInYear2023Dto
                                {
                                    IdOrder = torder.Id,
                                    StateOrder = tstateorder.Name,
                                    Comments = torder.Comments,
                                    OrderDate = torder.OrderDate
                                })
                                .ToListAsync();
            return Ok(results);
        }

        //Consulta 12: Devuelve un listado de todos los pedidos que han sido entregados en el mes de enero de cualquier año.

        [HttpGet("GetListOfOrderInJanuary_12")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<IEnumerable<ListCancelledInYear2023Dto>>> GetListOfOrderInJanuary()
        {
            var results = await (from torder in _context.Orders
                                join tstateorder in _context.StatesOrders on torder.IdStateOrderFk equals tstateorder.Id 
                                where tstateorder.Id == 10 && torder.ExpectedDate.Month == 1
                                select new ListCancelledInYear2023Dto
                                {
                                    IdOrder = torder.Id,
                                    StateOrder = tstateorder.Name,
                                    Comments = torder.Comments,
                                    OrderDate = torder.OrderDate
                                })
                                .ToListAsync();
            return Ok(results);
        }

        //Consulta 13: Devuelve un listado con todos los pagos que se realizaron en el año 2023 mediante Paypal. Ordene el resultado de mayor a menor.

        [HttpGet("GetListOfPayPalPayment_13")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<IEnumerable<ListOfPayPalPaymentDto>>> GetListOfPayPalPayment()
        {
            var results = await (from tpayment in _context.Payments
                                join tpaymetmeth in _context.PaymentsMethods on tpayment.IdPaymenMetFk equals tpaymetmeth.Id 
                                where  tpaymetmeth.MethodName == "PayPal" && tpayment.DatePayment.Year == 2023
                                select new ListOfPayPalPaymentDto
                                {
                                    MethodPayment = tpaymetmeth.MethodName,
                                    DatePayment = tpayment.DatePayment
                                })
                                .ToListAsync();
            return Ok(results);
        }

        //Columna 14:Devuelve un listado con todas las formas de pago que aparecen en la tabla pago. Tenga en cuenta que no deben aparecer formas de pago repetidas.


        [HttpGet("GetListAllMethodsOfPayment_14")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<IEnumerable<ListAllMethodsOfPaymentDto>>> GetListAllMethodsOfPayment()
        {
            var results = await (from tpayment in _context.Payments
                                join tpaymetmeth in _context.PaymentsMethods on tpayment.IdPaymenMetFk equals tpaymetmeth.Id
                                group tpayment by tpayment.IdPaymenMetFk into paymentGroup
                                select new ListAllMethodsOfPaymentDto
                                {
                                    MethodPayment = paymentGroup.First().PaymentsMethods.MethodName
                                })
                                .ToListAsync();
            return Ok(results);
        }

        //Columna 15:Devuelve un listado con todos los productos que pertenecen a la gama Garden Tools Set y que tienen más de 100 unidades en stock. El listado deberá estar ordenado por su precio de venta, mostrando en primer lugar los de mayor precio.  

        [HttpGet("GetListProductsGardenToolsSet_15")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<IEnumerable<ListProductsGardenToolSetDto>>> GetListProductsGardenToolsSet()
        {
            var results = await (from tproduct in _context.Products
                                join trangerproduct in _context.RangersProducts on tproduct.IdRangerFk equals trangerproduct.Id
                                where tproduct.IdRangerFk == "RNG002" && tproduct.Stock > 100
                                select new ListProductsGardenToolSetDto
                                {
                                    RanferProduct = trangerproduct.DescriptionText,
                                    Name = tproduct.Name,
                                    ProductPrice = tproduct.PriceSale
                                })
                                .OrderByDescending(p => p.ProductPrice)
                                .ToListAsync()
                                ;
            return Ok(results);
        }

        //Columna 16:Devuelve un listado con todos los clientes que sean de la ciudad de Madrid y cuyo representante de ventas tenga el código de empleado 11 o 30.
        //Se agregaron datos

        [HttpGet("GetClientsFromMadridAndEmployee(11And30)_16")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<IEnumerable<ClientsFromMadridAndEmployeeDto>>> GetClientsFromMadridAndEmployee()
        {
            var results = await _context.ClientsAddresses
                .Where(tclientaddress => tclientaddress.Cities.Name == "Madrid" &&
                                        (tclientaddress.Clients.Employees.Id == 11 || tclientaddress.Clients.Employees.Id == 30))
                .Select(tclientaddress => new ClientsFromMadridAndEmployeeDto
                {
                    NameCity = tclientaddress.Cities.Name,
                    ClientName = tclientaddress.Clients.Name,
                    IdEmployee = tclientaddress.Clients.Employees.Id
                })
                .ToListAsync();

            return Ok(results);
        }
        
        //Columna 17:Obtén un listado con el nombre de cada cliente y el nombre y apellido de su representante de ventas.

        [HttpGet("GetClientsAndSalesEmployee_17")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<IEnumerable<ClientsAndSalesEmployeeDto>>> GetClientsAndSalesEmployee()
        {
            var results = await (from tclient in _context.Clients
                                join temployee in _context.Employees on tclient.IdEmployeeFk equals temployee.Id
                                join tpositionemployee in _context.PositionsEmployees on temployee.IdPositionFk equals tpositionemployee.Id
                                where tpositionemployee.Id == 3
                                select new ClientsAndSalesEmployeeDto
                                {
                                    ClientName = tclient.Name,
                                    EmployeeName = temployee.Name,
                                    LastNameOne = temployee.LastNameOne,
                                    LastNameTwo = temployee.LastNameTwo
                                })
                                .ToListAsync()
                                ;
            return Ok(results);
        }
        //Columna 18:Muestra el nombre de los clientes que hayan realizado pagos junto con el nombre de sus representantes de ventas.

        [HttpGet("GetClientsDidPaymentsAndEmployeeSalesAssociate_18")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<IEnumerable<ClientsDidPaymentsAndEmployeesDto>>> GetClientsDidPaymentsAndEmployees()
        {
            var results = await (from tclient in _context.Clients
                                join temployee in _context.Employees on tclient.IdEmployeeFk equals temployee.Id
                                join tpositionemployee in _context.PositionsEmployees on temployee.IdPositionFk equals tpositionemployee.Id
                                join tpayment in _context.Payments on tclient.Id equals tpayment.IdClientFk
                                where tpayment.Total > 0 && tpositionemployee.Name == "Sales Associate"
                                select new ClientsDidPaymentsAndEmployeesDto
                                {
                                    ClientName = tclient.Name,
                                    EmployeeName = temployee.Name,
                                    LastNameOne = temployee.LastNameOne,
                                    LastNameTwo = temployee.LastNameTwo,
                                    PaymentsTotal = tpayment.Total,
                                    IdPayment = tpayment.Id
                                })
                                .ToListAsync()
                                ;
            return Ok(results);
        }

        //Columna 19:Muestra el nombre de los clientes que no hayan realizado pagos junto con el nombre de sus representantes de ventas.

        [HttpGet("GetClientsDontDidPaymentsAndEmployeeSalesAssociate_19")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<IEnumerable<ClientsDidPaymentsAndEmployeesDto>>> GetClientsDontDidPaymentsAndEmployeeSalesAssociate()
        {
            var results = await (from tclient in _context.Clients
                                join temployee in _context.Employees on tclient.IdEmployeeFk equals temployee.Id
                                join tpositionemployee in _context.PositionsEmployees on temployee.IdPositionFk equals tpositionemployee.Id
                                join tpayment in _context.Payments on tclient.Id equals tpayment.IdClientFk
                                where tpayment.Total == 0 && tpositionemployee.Name == "Sales Associate"
                                select new ClientsDidPaymentsAndEmployeesDto
                                {
                                    ClientName = tclient.Name,
                                    EmployeeName = temployee.Name,
                                    LastNameOne = temployee.LastNameOne,
                                    LastNameTwo = temployee.LastNameTwo,
                                    PaymentsTotal = tpayment.Total,
                                })
                                .ToListAsync()
                                ;
            return Ok(results);
        }


        //Columna 20: Devuelve el nombre de los clientes que han hecho pagos y el nombre de sus representantes junto con la ciudad de la oficina a la que pertenece el representante.

        [HttpGet("GetClientsDidPaymentsAndContactClient_20")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<IEnumerable<ClientsDidPaymentsAndContactClientDto>>> GetClientsDidPaymentsAndContactClient()
        {
            var results = await (from tclient in _context.Clients
                                join temployee in _context.Employees on tclient.IdEmployeeFk equals temployee.Id
                                join toffice in _context.Offices on  temployee.IdOfficeFk equals toffice.Id
                                join tofficeaddress in _context.OfficesAddresses on toffice.Id equals tofficeaddress.IdOfficeFk
                                join tcity in _context.Cities on tofficeaddress.IdCityFk equals tcity.Id
                                join tpositionemployee in _context.PositionsEmployees on temployee.IdPositionFk equals tpositionemployee.Id
                                join tpayment in _context.Payments on tclient.Id equals tpayment.IdClientFk
                                where tpayment.Total > 0 
                                select new ClientsDidPaymentsAndContactClientDto
                                {
                                    ClientName = tclient.Name,
                                    EmployeeName = temployee.Name,
                                    LastNameOne = temployee.LastNameOne,
                                    LastNameTwo = temployee.LastNameTwo,
                                    PaymentsTotal = tpayment.Total,
                                    CityOfOffice = tcity.Name
                                })
                                .ToListAsync()
                                ;
            return Ok(results);
        }

        //Columna 21: Devuelve el nombre de los clientes que no hayan hecho pagos y el nombre de sus representantes junto con la ciudad de la oficina a la que pertenece el representante.

        [HttpGet("GetClientsNotPaymentsAndContactClient_21")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<IEnumerable<ClientsDidPaymentsAndContactClientDto>>> ClientsNotPaymentsAndContactClient()
        {
            var results = await (from tclient in _context.Clients
                                join temployee in _context.Employees on tclient.IdEmployeeFk equals temployee.Id
                                join toffice in _context.Offices on  temployee.IdOfficeFk equals toffice.Id
                                join tofficeaddress in _context.OfficesAddresses on toffice.Id equals tofficeaddress.IdOfficeFk
                                join tcity in _context.Cities on tofficeaddress.IdCityFk equals tcity.Id
                                join tpositionemployee in _context.PositionsEmployees on temployee.IdPositionFk equals tpositionemployee.Id
                                join tpayment in _context.Payments on tclient.Id equals tpayment.IdClientFk
                                where tpayment.Total == 0 
                                select new ClientsDidPaymentsAndContactClientDto
                                {
                                    ClientName = tclient.Name,
                                    EmployeeName = temployee.Name,
                                    LastNameOne = temployee.LastNameOne,
                                    LastNameTwo = temployee.LastNameTwo,
                                    PaymentsTotal = tpayment.Total,
                                    CityOfOffice = tcity.Name
                                })
                                .ToListAsync()
                                ;
            return Ok(results);
        }

        //Columna 22: Lista la dirección de las oficinas que tengan clientes en Madrid.

        [HttpGet("GetOfficeInMadrid_22")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<IEnumerable<OfficeInMadridDto>>> GetOfficeInMadridt()
        {
            var results = await (from tofficeaddress in _context.OfficesAddresses
                                join tcity in _context.Cities on tofficeaddress.IdCityFk equals tcity.Id
                                join tclientaddress in _context.ClientsAddresses on tcity.Id equals tclientaddress.IdCityFk
                                join tclient in _context.Clients on  tclientaddress.IdClientFk equals tclient.Id
                                where tclientaddress.IdClientFk == 7
                                select new OfficeInMadridDto
                                {
                                    MainNumber = tofficeaddress.MainNumber,
                                    Letter = tofficeaddress.Letter,
                                    Bis = tofficeaddress.Bis,
                                    SecLet = tofficeaddress.SecLet,
                                    Cardinal = tofficeaddress.Cardinal,
                                    SecNum = tofficeaddress.SecNum,
                                    SecCard = tofficeaddress.SecCard,
                                    Complet = tofficeaddress.Complet,
                                    PosCod = tofficeaddress.PosCod,
                                    NameCity = tcity.Name,
                                    ClientsName = tclient.Name
                                })
                                .ToListAsync()
                                ;
            return Ok(results);
        }

        //Columna 23: Devuelve el nombre de los clientes y el nombre de sus representantes junto con la ciudad de la oficina a la que pertenece el representante.

        [HttpGet("GetClientEmployeeAndCity_23")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<IEnumerable<ClientEmployeeAndCityDto>>> GetClientEmployeeAndCity()
        {
            var results = await (from tclient in _context.Clients
                                join temployee in _context.Employees on tclient.IdEmployeeFk equals temployee.Id
                                join toffice in _context.Offices on temployee.IdOfficeFk equals toffice.Id
                                join tofficeaddress in _context.OfficesAddresses on toffice.Id equals tofficeaddress.IdOfficeFk
                                join tcity in _context.Cities on tofficeaddress.IdCityFk equals tcity.Id
                                select new ClientEmployeeAndCityDto
                                {
                                    ClientName = tclient.Name,
                                    PhoneNumber = tclient.PhoneNumber,
                                    NameEmployee = temployee.Name,
                                    LastNameOneEmployee = temployee.LastNameOne,
                                    LastNameTwoEmployee = temployee.LastNameTwo,
                                    CityOffice = tcity.Name
                                })
                                .ToListAsync()
                                ;
            return Ok(results);
        }

        //Columna 24: Devuelve un listado con el nombre de los empleados junto con el nombre de sus jefes.
        
        [HttpGet("GetEmployeesAndBoss_24")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<IEnumerable<EmployeesAndBossDto>>> GetEmployeesAndBoss()
        {
            var results = await (from temployee in _context.Employees
                                join tboss in _context.Bosses on temployee.IdBoosFk equals tboss.Id
                                select new EmployeesAndBossDto
                                {
                                    EmployeeName = temployee.Name,
                                    BossName = tboss.Name,
                                    IdBoosFk = tboss.Id
                                })
                                .ToListAsync()
                                ;
            return Ok(results);
        }

        //Columna 25: Devuelve un listado que muestre el nombre de cada empleados, el nombre de su jefe y el nombre del jefe de sus jefe.

        [HttpGet("GetEmployeesAndBossAndBosses_25")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<IEnumerable<EmployeesAndBossAndBossesDto>>> EmployeesAndBossAndBosses()
        {
            var results = await (from temployee in _context.Employees
                                join tboss in _context.Bosses on temployee.IdBoosFk equals tboss.Id
                                select new EmployeesAndBossAndBossesDto
                                {
                                    EmployeeName = temployee.Name,
                                    BossName = tboss.Name,
                                    IdBoosFk = tboss.Id
                                })
                                .ToListAsync()
                                ;
            return Ok(results);
        }

        //Columna 26: Devuelve el nombre de los clientes a los que no se les ha entregado a tiempo un pedido.
        [HttpGet("GetClientsWithDelayOrder_26")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<IEnumerable<ClientsWithDelayOrderDto>>> ClientsWithDelayOrder()
        {
            var results = await (from torder in _context.Orders
                                join tclient in _context.Clients on torder.IdClientFk equals tclient.Id
                                where (torder.ExpectedDate.DayNumber - torder.DeadlineDate.DayNumber ) > 0
                                select new ClientsWithDelayOrderDto
                                {
                                    NameClient = tclient.Name,
                                    IdOrder = torder.Id,
                                    DelayDays = (torder.ExpectedDate.DayNumber - torder.DeadlineDate.DayNumber)
                                })
                                .ToListAsync()
                                ;
            return Ok(results);
        }
}
