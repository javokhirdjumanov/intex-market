using IndexMarket.Application.DataTransferObject;
using IndexMarket.Application.Extantions;
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
        orderCreationDto.Product_Id.IsDefault();

        var storageProduct = await this.productRepository
            .SelectByIdWithDetailsAsync(
            expression: pro => pro.Id == orderCreationDto.Product_Id,
            includes: new string[]
            { 
                nameof(Product.Category),
                nameof(Product.ProductShape),
                nameof(Product.File)
            });

        if (storageProduct == null) 
            throw new NotFoundException("Product not found !");

        orderCreationDto.User_Id.IsDefault();

        var storageUser = await this.userRepository
            .SelectByIdWithDetailsAsync(
            expression: user => user.Id == orderCreationDto.User_Id,
            includes: new string[]
            {
                nameof(User.Address) 
            });

        if (storageUser == null)
            throw new NotFoundException("User not found !");

        orderCreationDto.Address_Id.IsDefault();

        if(storageUser.Address is null)
        {
            storageUser.Address = await this.addressRepository
                .SelectByIdAsync(orderCreationDto.Address_Id);
        }

        var order = new Order
        {
            Product = storageProduct,
            User = storageUser
        };

        var newOrder = await this.orderRepository
            .InsertAsync(order);

        var newConsultation = await this.consultationRepository
            .InsertAsync(
            new Consultation
            { 
                Order = newOrder
            });

        return this.orderFactory
            .MapToOrderDto(newOrder);
    }

    public IEnumerable<OrderDto> GetAllOrders()
    {
        var orders = this.orderRepository
            .SelectAll()
            .Include(x => x.Product).ThenInclude(p => p.File)
            .Include(x => x.User).ThenInclude(u => u.Address)
            .ToList();

        return orders
            .Select(x => this.orderFactory
            .MapToOrderDto(x));
    }

    public async ValueTask<OrderDto> GetOrderByIdAsync(Guid orderId)
    {
        orderId.IsDefault();

        var storageOrder = await this.orderRepository
            .SelectByIdWithDetailsAsync(
            expression: ord => ord.Id == orderId,
            includes: new string[]
            { 
                $"{nameof(Order.Product)}.{nameof(Product.File)}",
                $"{nameof(Order.User)}.{nameof(User.Address)}"
            });

        ValidationStorageObject
            .ValidationGeneric<Order>(storageOrder, orderId);

        return this.orderFactory
            .MapToOrderDto(storageOrder);
    }

    public IEnumerable<OrderDto> SearchOrders(string columnName)
    {
        var orders = this.orderRepository
            .SelectAll()
            .Include(x => x.Product).ThenInclude(p => p.File)
            .Include(x => x.Product.ProductShape)
            .Include(x => x.User).ThenInclude(u => u.Address)
            .ToList();

        var filterOrders = orders.Where(o => o.User.FirstName.ToLower().Contains(columnName.ToLower()) ||
                                    o.User.PhoneNumber.Contains(columnName) ||
                                    o.Product.ProductShape.Name.ToLower().Contains(columnName.ToLower()));

        return filterOrders
            .Select(x => this.orderFactory
            .MapToOrderDto(x));
    }

    public IQueryable<OrderDto> FilterOrdersByProductPrice_S(decimal? from_price, decimal? to_price)
    {
        var filterOrders = this.orderRepository.FilterOrdersByProductPrice_R(from_price, to_price);

        return filterOrders.Select(x => this.orderFactory.MapToOrderDtoForFilters(x));
    }

    public IQueryable<OrderDto> FilterOrdersByCreateAt(DateOnly from_date, DateOnly to_date)
    {
        var filterOrders = this.orderRepository.FilterOrdersByCreateAt_R(from_date, to_date);

        return filterOrders.Select(x => this.orderFactory.MapToOrderDtoForFilters(x));
    }

    public async ValueTask<OrderDto> DeleteOrdersAsync(Guid orderId)
    {
        orderId.IsDefault();

        var storageOrder = await this.orderRepository
            .SelectByIdWithDetailsAsync(
            expression: order => order.Id == orderId,
            includes: new string[] 
            { 
                $"{nameof(Order.Product)}.{nameof(Product.File)}", 
                $"{nameof(Order.User)}.{nameof(User.Address)}" 
            });

        ValidationStorageObject
            .ValidationGeneric<Order>(storageOrder, orderId);

        var removedOrder = await this.orderRepository
            .DeleteAsync(storageOrder);

        return this.orderFactory
            .MapToOrderDto(removedOrder);
    }

    public IQueryable<OrderReportDto> ReportOrdersWithQuantity(
        DateOnly from_date,
        DateOnly end_date,
        bool collect_quantity,
        long? quantity)
    {
        var report_Orders = this.orderRepository.ReportOrdersWithQuantity(from_date, end_date, collect_quantity, quantity);

        return report_Orders.Select(x => this.orderFactory.MapToReportDto(x));
    }
}
