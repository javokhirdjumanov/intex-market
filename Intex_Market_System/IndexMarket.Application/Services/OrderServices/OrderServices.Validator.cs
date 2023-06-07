using IndexMarket.Application.DataTransferObject;
using IndexMarket.Application.Validators;
using IndexMarket.Domain.Entities;
using IndexMarket.Domain.Exceptions;
using System.Text.Json;

namespace IndexMarket.Application.Services;
public partial class OrderServices
{
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
