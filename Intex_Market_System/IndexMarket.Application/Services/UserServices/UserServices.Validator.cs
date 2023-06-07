using FluentValidation.Results;
using IndexMarket.Application.DataTransferObject;
using IndexMarket.Application.Validators;
using IndexMarket.Domain.Entities;
using IndexMarket.Domain.Exceptions;
using System.Text.Json;

namespace IndexMarket.Application.Services;
public partial class UserServices
{
    public void ValidateCreationUserDto(UserForCreationDto userForCreationDto)
    {
        var validationResult = new UserForCreationDtoValidator()
            .Validate(userForCreationDto);

        ThrowValidationExceptionIfValidationIsInvalid(validationResult);
    }

    public void ValidateUserForModificationDto(
       UserForModificationDto userForModificationDto)
    {
        var validationResult = new UserForModificationDtoValidator()
            .Validate(userForModificationDto);

        ThrowValidationExceptionIfValidationIsInvalid(validationResult);
    }

    private static void ThrowValidationExceptionIfValidationIsInvalid(ValidationResult validationResult)
    {
        if (validationResult.IsValid)
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
