using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Process1.Data;
using Process1.Models;
using Process1.DTOs;
using Process1.Exceptions;
using Process1.Services;



namespace Process1.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CustomersController : ControllerBase
    {
        private readonly ICustomerService _customerService;

        public CustomersController(ICustomerService customerService)
        {
            _customerService = customerService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<CustomerDto>>> GetCustomers()
        {
            var customers = await _customerService.GetAllCustomersAsync();
            return Ok(customers);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<CustomerDto>> GetCustomer(int id)
        {
            var customer = await _customerService.GetCustomerByIdAsync(id);
            if (customer == null)
                return NotFound();

            return Ok(customer);
        }

        [HttpPost]
        public async Task<ActionResult<CustomerDto>> CreateCustomer(CreateCustomerDto customerDto)
        {
            var customer = await _customerService.CreateCustomerAsync(customerDto);
            return CreatedAtAction(nameof(GetCustomer), new { id = customer.CustomerID }, customer);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCustomer(int id, UpdateCustomerDto customerDto)
        {
            try
            {
                await _customerService.UpdateCustomerAsync(id, customerDto);
                return NoContent();
            }
            catch (NotFoundException)
            {
                return NotFound();
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCustomer(int id)
        {
            try
            {
                await _customerService.DeleteCustomerAsync(id);
                return NoContent();
            }
            catch (NotFoundException)
            {
                return NotFound();
            }
        }
    }
}