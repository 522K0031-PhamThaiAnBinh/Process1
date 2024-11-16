using Process1.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Process1.Services
{
    public interface IProductService
    {
        Task<IEnumerable<ProductDto>> GetAllProductsAsync();
        Task<ProductDto> GetProductByIdAsync(int id);
        Task<ProductDto> CreateProductAsync(CreateProductDto productDto);
        Task UpdateProductPriceAsync(int id, decimal price);
    }
}
