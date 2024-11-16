using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Process1.Data;
using Process1.Exceptions;
using Process1.Models;
using Process1.DTOs;
using Process1.Services;
namespace Process1.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController : ControllerBase
    {
        private readonly IProductService _productService;

        public ProductsController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductDto>>> GetProducts()
        {
            var products = await _productService.GetAllProductsAsync();
            return Ok(products);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ProductDto>> GetProduct(int id)
        {
            var product = await _productService.GetProductByIdAsync(id);
            if (product == null)
                return NotFound();

            return Ok(product);
        }

        [HttpPost]
        public async Task<ActionResult<ProductDto>> CreateProduct(CreateProductDto productDto)
        {
            var product = await _productService.CreateProductAsync(productDto);
            return CreatedAtAction(nameof(GetProduct), new { id = product.ProductID }, product);
        }

        [HttpPut("{id}/price")]
        public async Task<IActionResult> UpdatePrice(int id, UpdateProductPriceDto priceDto)
        {
            try
            {
                await _productService.UpdateProductPriceAsync(id, priceDto.Price);
                return NoContent();
            }
            catch (NotFoundException)
            {
                return NotFound();
            }
        }
    }
}