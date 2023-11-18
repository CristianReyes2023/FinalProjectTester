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

    [HttpGet("CityAndOfficePhoneCountry_2")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<IEnumerable<CityAndOfficePhoneCountryDto>>> CityAndOfficePhoneCountry(string country)
    {
        var results = await (from toffice in _context.Offices
                            join tofficeaddress in _context.OfficesAddresses on toffice.Id equals tofficeaddress.IdOfficeFk
                            join tcity in _context.Cities on tofficeaddress.IdCityFk equals tcity.Id
                            join tstate in _context.States on tcity.IdStateFk equals tstate.Id
                            join tcountry in _context.Countries on tstate.IdCountryFk equals tcountry.Id
                            where tcountry.Name == country
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
    public async Task<ActionResult<BossAndEmployeesDto>> GetEmployeeByBossId(int id)
    {
        var result = await _unitOfWork.Bosses.GetEmployeeByBossId(id);
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
    public async Task<ActionResult<IEnumerable<PositionNameEmailOfManagerDto>>> GetPositionNameEmailOfManager(string position)
    {
        var results = await (from temployee in _context.Employees
                             join tboss in _context.Bosses on temployee.IdBoosFk equals tboss.Id
                             join tposition in _context.PositionsEmployees on temployee.IdPositionFk equals tposition.Id
                             where tposition.Name == position
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

    [HttpGet("GetClientsByCountry_6")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<IEnumerable<ClientsByCountryDto>>> GetClientsByCountry(string country)
    {
        var results = await (from tclientaddress in _context.ClientsAddresses
                             join tclient in _context.Clients on tclientaddress.IdClientFk equals tclient.Id
                             join tcity in _context.Cities on tclientaddress.IdCityFk equals tcity.Id
                             join tstate in _context.States on tcity.IdStateFk equals tstate.Id
                             join tcountry in _context.Countries on tstate.IdCountryFk equals tcountry.Id
                             where tcountry.Name.Trim().ToLower() == country.Trim().ToLower()
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
                                join tclient in _context.Orders on torder.IdClientFk equals tclient.Id 
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

}
