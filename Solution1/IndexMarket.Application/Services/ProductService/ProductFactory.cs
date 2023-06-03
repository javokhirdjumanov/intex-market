using IndexMarket.Application.DataTransferObject;
using IndexMarket.Domain;
using IndexMarket.Domain.Entities;
using IndexMarket.Infrastructure.Repository;

namespace IndexMarket.Application.Services;
public class ProductFactory : IProductFactory
{
    public Product MapToProduct(
        ProductForCreationDto productForCreationDto,
        Category? maybeCategory,
        ProductShape? maybeShape)
    {
        decimal? sale = productForCreationDto.SalePrice;

        var status = ProductStatus.Not_Available;

        if (sale == default(decimal))
            status = ProductStatus.Recommended;
        else
            status = ProductStatus.Discount;

        var tempCategory = new Category();
        if(maybeCategory is not null)
        {
            tempCategory.Id = maybeCategory.Id;
            tempCategory.Title = maybeCategory.Title;
            tempCategory.CreatedAt = maybeCategory.CreatedAt;
            tempCategory.UpdatedAt = maybeCategory.UpdatedAt;
            tempCategory.Products = new List<Product>(maybeCategory.Products);
        }
        else
        {
            tempCategory.Title = productForCreationDto.category;
        }

        var tempShape = new ProductShape();
        if(maybeShape is not null)
        {
            tempShape.Id = maybeShape.Id;
            tempShape.Name = maybeShape.Name;
            tempShape.Products = new List<Product>(maybeShape.Products);
        }
        else
        {
            tempShape.Name = productForCreationDto.shape;
        }

        return new Product
        {
            PhotoLink = productForCreationDto.PhotoLink,
            SalePrice = sale,
            Price = productForCreationDto.Price,
            Amount = productForCreationDto.Amount,
            Height = productForCreationDto.Height,
            Depth = productForCreationDto.Depth,
            Status = status,
            Category = tempCategory,
            ProductShape = tempShape
        };
    }

    public void MapToProduct(Product storageProduct, ProductForModificationDto productForModificationDto)
    {
        throw new NotImplementedException();
    }
    public ProductDto MapToProductDto(Product product)
    {
        return null;
    }
}
