using IndexMarket.Application.DataTransferObject;
using IndexMarket.Application.Validators;
using IndexMarket.Domain.Entities;
using IndexMarket.Domain.Exceptions;
using System.Text.Json;

namespace IndexMarket.Application.Services;
public partial class OrderServices
{
    public void ValidationId(Guid orderId)
    {
        if(orderId == default)
        {
            throw new ValidationException($"The given userId is invalid: {orderId}");
        }
    }

    public void ValidationStorageOrder(Order order, Guid orderId)
    {
        if(order is null)
        {
            throw new NotFoundException($"Cound't order with given Id:{orderId}");
        }
    }

    public void ValidationCreation(OrderCreationDto orderCreationDto)
    {
        var validationResult = new OrderCreationValidator().Validate(orderCreationDto);

        if(validationResult.IsValid)
        {
            return;
        }
        var errors = JsonSerializer
                .Serialize(validationResult.Errors.Select(error => new
                {
                    PropertyName = error.PropertyName,
                    ErrorMessage = error.ErrorMessage,
                    AttemptedValue = error.AttemptedValue
                }));

        throw new ValidationException(errors);
    }
}
