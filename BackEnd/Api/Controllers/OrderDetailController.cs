using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Api.Dto;
using AutoMapper;
using Domain.Entities;
using Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    public class OrderDetailController : BaseController
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public OrderDetailController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<IEnumerable<OrderDetailDto>>> Get()
        {
            var results = await _unitOfWork.OrderDetails.GetAllAsync();
            return _mapper.Map<List<OrderDetailDto>>(results);
        }

        [HttpGet("{idOrderFk}/{idProductFk}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<OrderDetailDto>> Get(int idOrderFk, string idProductFk )
        {
            var result = await _unitOfWork.OrderDetails.GetByIdAsync(idOrderFk, idProductFk);
            if (result == null)
            {
                return NotFound();
            }
            return _mapper.Map<OrderDetailDto>(result);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<OrderDetailDto>> Post(OrderDetailDto resultDto)
        {
            var result = _mapper.Map<OrderDetail>(resultDto);
            _unitOfWork.OrderDetails.Add(result);
            await _unitOfWork.SaveAsync();
            if (result == null)
            {
                return BadRequest();
            }
            resultDto.IdOrderFk = result.IdOrderFk;
            resultDto.IdProductFk = result.IdProductFk;
            return CreatedAtAction(nameof(Post), new { idOrder = resultDto.IdOrderFk, idProduct = resultDto.IdProductFk }, resultDto);
        }

        [HttpPut("{idOrderFk}/{idProductFk}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<OrderDetailDto>> Put(int idOrderFk, string idProductFk, [FromBody] OrderDetailDto resultDto)
        {
            var result = await _unitOfWork.OrderDetails.GetByIdAsync(idOrderFk, idProductFk);
            if (result == null)
            {
                return NotFound();
            }
            // Update the properties of the existing entity with values from auditoriaDto
            _mapper.Map(resultDto, result);
            // The context is already tracking result, so no need to attach it
            await _unitOfWork.SaveAsync();
            // Return the updated entity
            return _mapper.Map<OrderDetailDto>(result);
        }

        [HttpDelete("{idOrderFk}/{idProductFk}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(int idOrderFk, string idProductFk)
        {
            var result = await _unitOfWork.OrderDetails.GetByIdAsync(idOrderFk, idProductFk);
            if (result == null)
            {
                return NotFound();
            }
            _unitOfWork.OrderDetails.Remove(result);
            await _unitOfWork.SaveAsync();
            return NoContent();
        }
    }
}