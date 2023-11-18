using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Api.Dto;
using AutoMapper;
using Domain.Entities;
using Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Persistence.Data;

namespace Api.Controllers
{
    public class CityController : BaseController
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly GardensContext _context;


        public CityController(IUnitOfWork unitOfWork, IMapper mapper,GardensContext context)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _context = context;

        }
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<IEnumerable<CityDto>>> Get()
        {
            var results = await _unitOfWork.Cities.GetAllAsync();
            return _mapper.Map<List<CityDto>>(results);
        }

        [HttpGet("CityAndOfficePhoneCountry")]
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

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<CityDto>> Get(int id)
        {
            var result = await _unitOfWork.Cities.GetByIdAsync(id);
            if (result == null)
            {
                return NotFound();
            }
            return _mapper.Map<CityDto>(result);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<CityDto>> Post(CityDto resultDto)
        {
            var result = _mapper.Map<City>(resultDto);
            _unitOfWork.Cities.Add(result);
            await _unitOfWork.SaveAsync();
            if (result == null)
            {
                return BadRequest();
            }
            resultDto.Id = result.Id;
            return CreatedAtAction(nameof(Post), new { id = resultDto.Id }, resultDto);
        }

        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<CityDto>> Put(int id, [FromBody] CityDto resultDto)
        {
            var result = await _unitOfWork.Cities.GetByIdAsync(id);
            if (result == null)
            {
                return NotFound();
            }
            if (resultDto.Id == 0)
            {
                resultDto.Id = id;
            }
            if (resultDto.Id != id)
            {
                return BadRequest();
            }
            // Update the properties of the existing entity with values from auditoriaDto
            _mapper.Map(resultDto, result);
            // The context is already tracking result, so no need to attach it
            await _unitOfWork.SaveAsync();
            // Return the updated entity
            return _mapper.Map<CityDto>(result);
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _unitOfWork.Cities.GetByIdAsync(id);
            if (result == null)
            {
                return NotFound();
            }
            _unitOfWork.Cities.Remove(result);
            await _unitOfWork.SaveAsync();
            return NoContent();
        }
    }
}