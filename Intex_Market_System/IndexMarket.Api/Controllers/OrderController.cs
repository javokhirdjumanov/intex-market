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

    [HttpGet("All")]
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

    [HttpGet("columnName")]
    public IActionResult GetSearchOrders(string columnName)
    {
        var orders = this.orderServices.SearchOrders(columnName);

        return Ok(orders);
    }

    [HttpDelete("{orderId:guid}")]
    public async ValueTask<ActionResult<OrderDto>> DeleteOrderAsync(Guid orderId)
    {
        var removeOrder = await this.orderServices.DeleteOrdersAsync(orderId);

        return Ok(removeOrder);
    }
}