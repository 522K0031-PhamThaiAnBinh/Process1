using System.ComponentModel.DataAnnotations;

namespace Process1.DTOs
{
    public class ProductDto
    {
        public int ProductID { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
    }

    public class CreateProductDto
    {
        [Required]
        [StringLength(100)]
        public string Name { get; set; }

        [Required]
        [Range(0.01, 999999.99)]
        public decimal Price { get; set; }
    }

    public class UpdateProductPriceDto
    {
        [Required]
        [Range(0.01, 999999.99)]
        public decimal Price { get; set; }
    }
}
