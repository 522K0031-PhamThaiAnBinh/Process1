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
    public class OrdersController : ControllerBase
    {
        private readonly IOrderService _orderService;

        public OrdersController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<OrderDto>>> GetOrders()
        {
            var orders = await _orderService.GetAllOrdersAsync();
            return Ok(orders);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<OrderDto>> GetOrder(int id)
        {
            var order = await _orderService.GetOrderByIdAsync(id);
            if (order == null)
                return NotFound();

            return Ok(order);
        }

        [HttpPost]
        public async Task<ActionResult<OrderDto>> CreateOrder(CreateOrderDto orderDto)
        {
            var order = await _orderService.CreateOrderAsync(orderDto);
            return CreatedAtAction(nameof(GetOrder), new { id = order.OrderID }, order);
        }

        [HttpPost("{id}/items")]
        public async Task<IActionResult> AddOrderItem(int id, AddOrderItemDto itemDto)
        {
            try
            {
                await _orderService.AddOrderItemAsync(id, itemDto);
                return NoContent();
            }
            catch (NotFoundException)
            {
                return NotFound();
            }
        }

        [HttpDelete("{orderId}/items/{productId}")]
        public async Task<IActionResult> RemoveOrderItem(int orderId, int productId)
        {
            try
            {
                await _orderService.RemoveOrderItemAsync(orderId, productId);
                return NoContent();
            }
            catch (NotFoundException)
            {
                return NotFound();
            }
        }
    }
}