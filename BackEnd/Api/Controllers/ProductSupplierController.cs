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
    public class ProductSupplierController : BaseController
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public ProductSupplierController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<IEnumerable<ProductSupplierDto>>> Get()
        {
            var results = await _unitOfWork.ProductSuppliers.GetAllAsync();
            return _mapper.Map<List<ProductSupplierDto>>(results);
        }

        [HttpGet("{idProductFk}/{idSupplierFk}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ProductSupplierDto>> Get(string idProductFk, int idSupplierFk )
        {
            var result = await _unitOfWork.ProductSuppliers.GetByIdAsync(idProductFk, idSupplierFk);
            if (result == null)
            {
                return NotFound();
            }
            return _mapper.Map<ProductSupplierDto>(result);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<ProductSupplierDto>> Post(ProductSupplierDto resultDto)
        {
            var result = _mapper.Map<ProductSupplier>(resultDto);
            _unitOfWork.ProductSuppliers.Add(result);
            await _unitOfWork.SaveAsync();
            if (result == null)
            {
                return BadRequest();
            }
            resultDto.IdSupplierFk = result.IdSupplierFk;
            resultDto.IdProductFk = result.IdProductFk;
            return CreatedAtAction(nameof(Post), new { idSupplier = resultDto.IdSupplierFk, idProduct = resultDto.IdProductFk }, resultDto);
        }

        [HttpPut("{idProductFk}/{idSupplierFk}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ProductSupplierDto>> Put(int idSupplierFk, string idProductFk, [FromBody] ProductSupplierDto resultDto)
        {
            var result = await _unitOfWork.ProductSuppliers.GetByIdAsync(idProductFk, idSupplierFk);
            if (result == null)
            {
                return NotFound();
            }
            // Update the properties of the existing entity with values from auditoriaDto
            _mapper.Map(resultDto, result);
            // The context is already tracking result, so no need to attach it
            await _unitOfWork.SaveAsync();
            // Return the updated entity
            return _mapper.Map<ProductSupplierDto>(result);
        }

        [HttpDelete("{idProductFk}/{idSupplierFk}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(int idSupplierFk, string idProductFk)
        {
            var result = await _unitOfWork.ProductSuppliers.GetByIdAsync(idProductFk, idSupplierFk);
            if (result == null)
            {
                return NotFound();
            }
            _unitOfWork.ProductSuppliers.Remove(result);
            await _unitOfWork.SaveAsync();
            return NoContent();
        }
    }
}