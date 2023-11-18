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
    public class EmployeeController : BaseController
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly GardensContext _context;


        public EmployeeController(IUnitOfWork unitOfWork, IMapper mapper,GardensContext context)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _context = context;

        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<IEnumerable<EmployeeDto>>> Get()
        {
            var results = await _unitOfWork.Employees.GetAllAsync();
            return _mapper.Map<List<EmployeeDto>>(results);
        }

        [HttpGet("GetPositionNameEmailOfManager")]
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

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<EmployeeDto>> Get(int id)
        {
            var result = await _unitOfWork.Employees.GetByIdAsync(id);
            if (result == null)
            {
                return NotFound();
            }
            return _mapper.Map<EmployeeDto>(result);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<EmployeeDto>> Post(EmployeeDto resultDto)
        {
            var result = _mapper.Map<Employee>(resultDto);
            _unitOfWork.Employees.Add(result);
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
        public async Task<ActionResult<EmployeeDto>> Put(int id, [FromBody] EmployeeDto resultDto)
        {
            var result = await _unitOfWork.Employees.GetByIdAsync(id);
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
            return _mapper.Map<EmployeeDto>(result);
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _unitOfWork.Employees.GetByIdAsync(id);
            if (result == null)
            {
                return NotFound();
            }
            _unitOfWork.Employees.Remove(result);
            await _unitOfWork.SaveAsync();
            return NoContent();
        }
    }
}