using IndexMarket.Application.DataTransferObject;
using IndexMarket.Domain.Entities;
using IndexMarket.Domain.Exceptions;
using IndexMarket.Infrastructure.Repository;
using Microsoft.EntityFrameworkCore;

namespace IndexMarket.Application.Services;
public partial class OrderServices : IOrderServices
{
    private readonly IOrderRepository orderRepository;
    private readonly IOrderFactory orderFactory;
    private readonly IProductRepository productRepository;
    private readonly IUserRepository userRepository;
    private readonly IAddressRepository addressRepository;
    private readonly IConsultationRepository consultationRepository;
    public OrderServices(
        IOrderRepository orderRepository,
        IProductRepository productRepository,
        IUserRepository userRepository,
        IAddressRepository addressRepository,
        IOrderFactory orderFactory,
        IConsultationRepository consultationRepository)
    {
        this.orderRepository = orderRepository;
        this.productRepository = productRepository;
        this.userRepository = userRepository;
        this.addressRepository = addressRepository;
        this.orderFactory = orderFactory;
        this.consultationRepository = consultationRepository;
    }

    public async ValueTask<OrderDto> CreateOrderAsync(OrderCreationDto orderCreationDto)
    {
        ValidationId(orderCreationDto.Product_Id);
        var storageProduct = await this.productRepository.SelectByIdWithDetailsAsync(
            expression: pro => pro.Id == orderCreationDto.Product_Id,
            includes: new string[] { nameof(Product.Category), nameof(Product.ProductShape) });

        if (storageProduct == null) 
            throw new NotFoundException("Product not found !");

        ValidationId(orderCreationDto.User_Id);
        var storageUser = await this.userRepository.SelectByIdWithDetailsAsync(
            expression: user => user.Id == orderCreationDto.User_Id,
            includes: new string[] { nameof(User.Address) });

        if (storageUser == null)
            throw new NotFoundException("User not found !");

        ValidationId(orderCreationDto.Address_Id);
        if(storageUser.Address is null)
        {
            storageUser.Address = await this.addressRepository.SelectByIdAsync(orderCreationDto.Address_Id);
        }

        var order = new Order
        {
            Product = storageProduct,
            User = storageUser
        };

        var newOrder = await this.orderRepository.InsertAsync(order);

        var newConsultation = await this.consultationRepository
            .InsertAsync(
            new Consultation
            { 
                Order = newOrder
            });

        return this.orderFactory.MapToOrderDto(newOrder);
    }

    public IEnumerable<OrderDto> GetAllOrders()
    {
        var orders = this.orderRepository
            .SelectAll()
            .Include(x => x.Product)
            .Include(x => x.User)
            .Include(x => x.User.Address)
            .ToList();

        return orders.Select(x => this.orderFactory.MapToOrderDto(x));
    }

    public async ValueTask<OrderDto> GetOrderByIdAsync(Guid orderId)
    {
        ValidationId(orderId);

        var order = await this.orderRepository.SelectByIdWithDetailsAsync(
            expression: ord => ord.Id == orderId,
            includes: new string[] { nameof(Order.Product), $"{nameof(Order.User)}.{nameof(User.Address)}"});

        ValidationStorageOrder(order, orderId);

        return this.orderFactory.MapToOrderDto(order);
    }

    public async ValueTask<OrderDto> DeleteOrdersAsync(Guid orderId)
    {
        ValidationId(orderId);

        var storageOrder = await this.orderRepository.SelectByIdWithDetailsAsync(
            expression: order => order.Id == orderId,
            includes: new string[] { nameof(Order.Product), $"{nameof(Order.User)}.{nameof(User.Address)}" });

        ValidationStorageOrder(storageOrder, orderId);

        var removedOrder = await this.orderRepository.DeleteAsync(storageOrder);

        return this.orderFactory.MapToOrderDto(removedOrder);
    }
}
