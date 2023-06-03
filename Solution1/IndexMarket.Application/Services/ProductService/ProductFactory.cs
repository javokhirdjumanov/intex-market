using IndexMarket.Application.DataTransferObject;
using IndexMarket.Domain;
using IndexMarket.Domain.Entities;

namespace IndexMarket.Application.Services;
public class ProductFactory : IProductFactory
{
    public Product MapToProduct(ProductForCreationDto productForCreationDto, Category maybeCategory)
    {
        var product = new Product();

        decimal? sale = productForCreationDto.SalePrice;

        product.PhotoLink = productForCreationDto.PhotoLink;
        product.SalePrice = sale;
        product.Price = productForCreationDto.Price;
        product.Amount = productForCreationDto.Amount;
        product.Height = productForCreationDto.Height;
        product.Depth = productForCreationDto.Depth;
        product.Category = maybeCategory;

        product.Status = (sale == null) 
            ? ProductStatus.Recommended 
            : ProductStatus.Discount;
        
        product.ProductShape = new ProductShape
        {
            Name = productForCreationDto.Shape
        };

        return product;
    }
    public void MapToProduct(Product storageProduct, ProductForModificationDto productForModificationDto)
    {
        throw new NotImplementedException();
    }
    public ProductDto MapToProductDto(Product product)
    {
        ProductShapeDto? productShape = default;
        if (product.ProductShape is not null)
            productShape = new ProductShapeDto(product.ProductShape.Id, product.ProductShape.Name);

        CategoryDto? category = default;
        if(product.Category is not null)
            category = new CategoryDto(product.Category.Id, product.Category.Title);

        return new ProductDto(
            productId: product.Id,
            PhotoLink: product.PhotoLink,
            SalePrice: product.SalePrice,
            Price: product.Price,
            Amount: product.Amount,
            Shape: productShape,
            Height: product.Height,
            Weight: product.Weight,
            Depth: product.Depth,
            Status: Enum.GetName(typeof(ProductStatus), product.Status),
            Category: category);
    }
}
