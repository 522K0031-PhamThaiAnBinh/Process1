using Process1.Data;
using Process1.DTOs;
using Process1.Exceptions;
using Process1.Models;
using Microsoft.EntityFrameworkCore;
using AutoMapper;

namespace Process1.Services
{
    public class OrderService : IOrderService
    {
        private readonly ECommerceContext _context;
        private readonly IMapper _mapper;

        public OrderService(ECommerceContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IEnumerable<OrderDto>> GetAllOrdersAsync()
        {
            var orders = await _context.Orders
                .Include(o => o.OrderItems)
                .ThenInclude(oi => oi.Product)
                .ToListAsync();

            return _mapper.Map<IEnumerable<OrderDto>>(orders);
        }

        public async Task<OrderDto> GetOrderByIdAsync(int id)
        {
            var order = await _context.Orders
                .Include(o => o.OrderItems)
                .ThenInclude(oi => oi.Product)
                .FirstOrDefaultAsync(o => o.OrderID == id);

            if (order == null)
                throw new NotFoundException($"Order with ID {id} not found");

            return _mapper.Map<OrderDto>(order);
        }

        public async Task<OrderDto> CreateOrderAsync(CreateOrderDto orderDto)
        {
            var order = _mapper.Map<Order>(orderDto);
            order.OrderDate = DateTime.UtcNow;
            order.Status = "Pending";

            _context.Orders.Add(order);
            await _context.SaveChangesAsync();

            return _mapper.Map<OrderDto>(order);
        }

        public async Task AddOrderItemAsync(int orderId, AddOrderItemDto itemDto)
        {
            var order = await _context.Orders
                .Include(o => o.OrderItems)
                .FirstOrDefaultAsync(o => o.OrderID == orderId);

            if (order == null)
                throw new NotFoundException($"Order with ID {orderId} not found");

            var product = await _context.Products.FindAsync(itemDto.ProductID);
            if (product == null)
                throw new NotFoundException($"Product with ID {itemDto.ProductID} not found");

            var orderItem = new OrderItem
            {
                OrderID = orderId,
                ProductID = itemDto.ProductID,
                Quantity = itemDto.Quantity,
                Subtotal = product.Price * itemDto.Quantity
            };

            order.OrderItems.Add(orderItem);
            await _context.SaveChangesAsync();
        }

        public async Task RemoveOrderItemAsync(int orderId, int productId)
        {
            var orderItem = await _context.OrderItems
                .FirstOrDefaultAsync(oi => oi.OrderID == orderId && oi.ProductID == productId);

            if (orderItem == null)
                throw new NotFoundException($"Order item not found");

            _context.OrderItems.Remove(orderItem);
            await _context.SaveChangesAsync();
        }
    }
}
