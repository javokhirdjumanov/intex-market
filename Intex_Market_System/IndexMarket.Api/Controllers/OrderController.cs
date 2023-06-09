using IndexMarket.Application.DataTransferObject;
using IndexMarket.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace IndexMarket.Api.Controllers;
[Route("api/[controller]")]
[ApiController]
public class OrderController : ControllerBase
{
    private readonly IOrderServices orderServices;
    public OrderController(IOrderServices orderServices)
    {
        this.orderServices = orderServices;
    }

    [HttpPost]
    public async ValueTask<ActionResult<OrderDto>> PostOrderAsync(OrderCreationDto orderCreationDto)
    {
        var order = await this.orderServices.CreateOrderAsync(orderCreationDto);

        return Created("", order);
    }

    [HttpGet("all")]
    public IActionResult GetAllOrders()
    {
        var orders = this.orderServices.GetAllOrders();

        return Ok(orders);
    }

    [HttpGet("{orderId:guid}")]
    public async ValueTask<ActionResult<OrderDto>> GetOrderByIdAsync(Guid orderId)
    {
        var order = await this.orderServices.GetOrderByIdAsync(orderId);

        return Ok(order);
    }

    [HttpGet("search")]
    public IActionResult GetSearchOrders(string value)
    {
        var orders = this.orderServices.SearchOrders(value);

        return Ok(orders);
    }

    [HttpGet("Form price To price/orders")]
    public IActionResult FilterOrdersByProductPrice(decimal? from_price, decimal? to_price)
    {
        var filterOrders = this.orderServices.FilterOrdersByProductPrice_S(from_price, to_price);

        return Ok(filterOrders);
    }

    [HttpDelete("{orderId:guid}")]
    public async ValueTask<ActionResult<OrderDto>> DeleteOrderAsync(Guid orderId)
    {
        var removeOrder = await this.orderServices.DeleteOrdersAsync(orderId);

        return Ok(removeOrder);
    }
}