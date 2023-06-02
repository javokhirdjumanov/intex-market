using IndexMarket.Application.DataTransferObject;
using IndexMarket.Application.Validators;
using IndexMarket.Domain.Entities;
using IndexMarket.Domain.Exceptions;
using System.Text.Json;

namespace IndexMarket.Application.Services;
public partial class CategoryServices
{
    public void ValidationCategoryId(Guid categoryId)
    {
        if(categoryId == default(Guid))
        {
            throw new ValidationException($"The given userId is invalid: {categoryId}");
        }
    }

    public void ValidationCategoryName(string categoryName)
    {
        if (categoryName is null)
        {
            throw new ValidationException($"The categoryName cannot null.!");
        }
    }

    public void ValidationStorageCategory(Category storageCategory, Guid categoryId) 
    {
        if(storageCategory is null)
        {
            throw new NotFoundException($"Couldn't find user with given id: {categoryId}");
        }
    }

    public void ValidationCategoryForModify(CategoryModifyDto categoryModifyDto)
    {
        var validationResult = new CategoryModifyValidator().Validate(categoryModifyDto);

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
