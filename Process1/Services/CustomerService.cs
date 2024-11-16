using Process1.Data;
using Process1.DTOs;
using Process1.Exceptions;
using Process1.Models;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace Process1.Services

{
    public class CustomerService : ICustomerService
    {
        private readonly ECommerceContext _context;
        private readonly IMapper _mapper;

        public CustomerService(ECommerceContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IEnumerable<CustomerDto>> GetAllCustomersAsync()
        {
            var customers = await _context.Customers.ToListAsync();
            return _mapper.Map<IEnumerable<CustomerDto>>(customers);
        }

        public async Task<CustomerDto> GetCustomerByIdAsync(int id)
        {
            var customer = await _context.Customers.FindAsync(id);
            if (customer == null)
                throw new NotFoundException($"Customer with ID {id} not found");

            return _mapper.Map<CustomerDto>(customer);
        }

        public async Task<CustomerDto> CreateCustomerAsync(CreateCustomerDto customerDto)
        {
            var customer = _mapper.Map<Customer>(customerDto);
            _context.Customers.Add(customer);
            await _context.SaveChangesAsync();

            return _mapper.Map<CustomerDto>(customer);
        }

        public async Task UpdateCustomerAsync(int id, UpdateCustomerDto customerDto)
        {
            var customer = await _context.Customers.FindAsync(id);
            if (customer == null)
                throw new NotFoundException($"Customer with ID {id} not found");

            _mapper.Map(customerDto, customer);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteCustomerAsync(int id)
        {
            var customer = await _context.Customers.FindAsync(id);
            if (customer == null)
                throw new NotFoundException($"Customer with ID {id} not found");

            _context.Customers.Remove(customer);
            await _context.SaveChangesAsync();
        }
    }
}