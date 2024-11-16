using System.ComponentModel.DataAnnotations;

namespace Process1.DTOs
{
    public class OrderDto
    {
        public int OrderID { get; set; }
        public int CustomerID { get; set; }
        public DateTime OrderDate { get; set; }
        public string Status { get; set; }
        public List<OrderItemDto> OrderItems { get; set; }
        public decimal Total { get; set; }
    }

    public class CreateOrderDto
    {
        [Required]
        public int CustomerID { get; set; }
        public List<OrderItemDto> OrderItems { get; set; }
    }

    public class OrderItemDto
    {
        public int ProductID { get; set; }
        public int Quantity { get; set; }
        public decimal Subtotal { get; set; }
    }

    public class AddOrderItemDto
    {
        [Required]
        public int ProductID { get; set; }

        [Required]
        [Range(1, 1000)]
        public int Quantity { get; set; }
    }
}
