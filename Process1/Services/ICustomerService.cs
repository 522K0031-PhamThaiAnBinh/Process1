using Process1.DTOs;
namespace Process1.Services

{
    public interface ICustomerService
    {
        Task<IEnumerable<CustomerDto>> GetAllCustomersAsync();
        Task<CustomerDto> GetCustomerByIdAsync(int id);
        Task<CustomerDto> CreateCustomerAsync(CreateCustomerDto customerDto);
        Task UpdateCustomerAsync(int id, UpdateCustomerDto customerDto);
        Task DeleteCustomerAsync(int id);
    }
}
