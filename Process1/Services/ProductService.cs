using Process1.DTOs;
using Process1.Models;
using Process1.Data;
using Process1.Exceptions;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Process1.Services
{
    public class ProductService : IProductService
    {
        private readonly ECommerceContext _context;

        public ProductService(ECommerceContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<ProductDto>> GetAllProductsAsync()
        {
            var products = await _context.Products.ToListAsync();
            return products.Select(p => new ProductDto
            {
                Name = p.Name,
                Price = p.Price,
            });
        }

        public async Task<ProductDto> GetProductByIdAsync(int id)
        {
            var product = await _context.Products.FindAsync(id);
            if (product == null)
            {
                return null;
            }

            return new ProductDto
            {
               
                Name = product.Name,
                Price = product.Price,
            };
        }

        public async Task<ProductDto> CreateProductAsync(CreateProductDto productDto)
        {
            var product = new Product
            {
                Name = productDto.Name,
                Price = productDto.Price,
            };

            _context.Products.Add(product);
            await _context.SaveChangesAsync();

            return new ProductDto
            {
      
                Name = product.Name,
                Price = product.Price,
            };
        }

        public async Task UpdateProductPriceAsync(int id, decimal price)
        {
            var product = await _context.Products.FindAsync(id);
            if (product == null)
            {
                throw new NotFoundException($"Product with ID {id} not found.");
            }

            product.Price = price;
            _context.Products.Update(product);
            await _context.SaveChangesAsync();
        }
    }
}
