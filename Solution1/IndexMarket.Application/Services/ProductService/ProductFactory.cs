using IndexMarket.Application.DataTransferObject;
using IndexMarket.Domain;
using IndexMarket.Domain.Entities;

namespace IndexMarket.Application.Services;
public class ProductFactory : IProductFactory
{
    public Product MapToProduct(
        ProductForCreationDto productForCreationDto,
        Category maybeCategory,
        ProductShape productShape)
    {
        return new Product
        {
            PhotoLink = productForCreationDto.PhotoLink,
            SalePrice = productForCreationDto.SalePrice,
            Status = (productForCreationDto.SalePrice == null)
                ? ProductStatus.Recommended
                : ProductStatus.Discount,
            Price = productForCreationDto.Price,
            Amount = productForCreationDto.Amount,
            Height = productForCreationDto.Height,
            Depth = productForCreationDto.Depth,
            Category = maybeCategory,
            ProductShape = productShape
        };
    }

    public Product MapToProduct(
        ProductForCreationDtoRectangel productForCreationDtoRectangel,
        Category category,
        ProductShape productShape)
    {
        return new Product
        {
            PhotoLink = productForCreationDtoRectangel.PhotoLink,
            SalePrice = productForCreationDtoRectangel.SalePrice,
            Status = (productForCreationDtoRectangel.SalePrice == null) 
                ? ProductStatus.Recommended 
                : ProductStatus.Discount,
            Price = productForCreationDtoRectangel.Price,
            Amount = productForCreationDtoRectangel.Amount,
            Height = productForCreationDtoRectangel.Height,
            Weight = productForCreationDtoRectangel.Weight,
            Depth = productForCreationDtoRectangel.Depth,
            Category = category,
            ProductShape = productShape
        };
    }

    public void MapToProduct(
        Product storageProduct,
        ProductForModificationDto productForModificationDto)
    {
        storageProduct.PhotoLink = productForModificationDto.PhotoLink ?? storageProduct.PhotoLink;
        storageProduct.Price = productForModificationDto.Price ?? storageProduct.Price;
        storageProduct.Amount = productForModificationDto.Amount ?? storageProduct.Amount;
        storageProduct.Height = productForModificationDto.Height ?? storageProduct.Height;

        if(storageProduct.ProductShape.Name == Shapes.Rectangle)
            storageProduct.Weight = productForModificationDto.Weight ?? storageProduct.Weight;

        if(productForModificationDto.SalePrice != null && productForModificationDto.SalePrice > 0)
            storageProduct.Status = ProductStatus.Discount;

        if (productForModificationDto.SalePrice == 0)
            storageProduct.Status = ProductStatus.Recommended;

        storageProduct.Depth = productForModificationDto.Depth ?? storageProduct.Depth;
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
