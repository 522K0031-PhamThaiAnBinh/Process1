using Process1.Data;
using Process1.DTOs;
using Process1.Exceptions;
using Process1.Models;

namespace Process1.Services
{
    public interface IOrderService
    {
        Task<IEnumerable<OrderDto>> GetAllOrdersAsync();
        Task<OrderDto> GetOrderByIdAsync(int id);
        Task<OrderDto> CreateOrderAsync(CreateOrderDto orderDto);
        Task AddOrderItemAsync(int orderId, AddOrderItemDto itemDto);
        Task RemoveOrderItemAsync(int orderId, int productId);
    }
}
