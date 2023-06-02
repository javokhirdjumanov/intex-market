using IndexMarket.Application.DataTransferObject;
using IndexMarket.Domain.Entities;
using IndexMarket.Domain.Enums;

namespace IndexMarket.Application.Services;
public class ProductFactory : IProductFactory
{
    public Product MapToProduct(ProductForCreationDto productForCreationDto)
    {
        double? tempWeight = productForCreationDto.Weight;

        var type = ProductType.Ramkali;
        if (tempWeight == null)
        {
            type = ProductType.Shishirilgan;
        }

        return new Product
        {
            PhotoLink = productForCreationDto.PhotoLink,
            Price = productForCreationDto.Price,
            Amount = productForCreationDto.Amount,
            Frame = productForCreationDto.Frame,
            Height = productForCreationDto.Height,
            Weight = tempWeight,
            Category = new Category
            {
                Title = Enum.GetName(typeof(ProductType), type)
            },
            Type = type
        };
    }

    public void MapToProduct(Product storageProduct, ProductForModificationDto productForModificationDto)
    {
        throw new NotImplementedException();
    }

    public ProductDto MapToProductDto(Product product)
    {
        var category = new CategoryDto(id: product.Category.Id, Title: product.Category.Title);

        return new ProductDto(
            PhotoLink: product.PhotoLink,
            Price: product.Price,
            Amount: product.Amount,
            Frame: product.Frame,
            Height: product.Height,
            Weight: product.Weight,
            Category: category,
            ProductType: category.Title);
    }
}
