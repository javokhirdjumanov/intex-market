using FluentValidation.Results;
using IndexMarket.Application.DataTransferObject;
using IndexMarket.Application.Validator;
using IndexMarket.Application.Validators;
using IndexMarket.Domain;
using IndexMarket.Domain.Entities;
using IndexMarket.Domain.Exceptions;
using System.Data.SqlTypes;
using System.Diagnostics;
using System.Text.Json;

namespace IndexMarket.Application.Services;
public partial class ProductServices
{
    public void ValidationGeneric<TEntity> (TEntity entity)
    {
        if(entity == null)
        {
            throw new NotFoundException("Don't found object !");
        }
    }

    public void ValidationNotRectangel(ProductShape productShape)
    {
        if(productShape.Name == Shapes.Rectangle)
        {
            throw new InvalidShape("The shape cannot be Rectangel !");
        }
    }

    public void ValidationRectangel(ProductShape shape)
    {
        if(shape.Name != Shapes.Rectangle)
        {
            throw new InvalidShape("This is shape is not Rectangel !");
        }
    }

    public void ValidationProductId(Guid productId)
    {
        if(productId == default)
        {
            throw new ValidationException($"The given userId is invalid: {productId}");
        }
    }

    public void ValidationStorageProduct(Product storageProduct, Guid productId)
    {
        if (storageProduct is null)
        {
            throw new NotFoundException($"Couldn't find user with given id: {productId}");
        }
    }

    public void ValidateCreationProductDto(ProductForCreationDto productForCreationDto)
    {
        var validationResult = new ProductCreationValidator().Validate(productForCreationDto);

        ThrowValidationExceptionIfValidationIsInvalid(validationResult);
    }

    public void ValidationCreationRectangleProductDto(ProductForCreationDtoRectangel productForCreationDtoRectangel)
    {
        var validationResult = new ProductCreationValidatorForRectangel().Validate(productForCreationDtoRectangel);

        ThrowValidationExceptionIfValidationIsInvalid(validationResult);
    }

    public void ValidateModificationProductDto(ProductForModificationDto productForModificationDto)
    {
        var validationResult = new ProductModificationValidator().Validate(productForModificationDto);

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
