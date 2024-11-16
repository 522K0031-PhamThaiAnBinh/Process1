// Mapping/AutoMapperProfile.cs
using Process1.DTOs;
using Process1.Models;
using AutoMapper;

public class AutoMapperProfile : Profile
{
    public AutoMapperProfile()
    {
        CreateMap<Customer, CustomerDto>();
        CreateMap<CreateCustomerDto, Customer>();
        CreateMap<UpdateCustomerDto, Customer>();

        CreateMap<Product, ProductDto>();
        CreateMap<CreateProductDto, Product>();

        CreateMap<Order, OrderDto>()
            .ForMember(dest => dest.Total,
                opt => opt.MapFrom(src => src.OrderItems.Sum(oi => oi.Subtotal)));
        CreateMap<CreateOrderDto, Order>();

        CreateMap<OrderItem, OrderItemDto>();
    }
}