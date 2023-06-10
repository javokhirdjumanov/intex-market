using IndexMarket.Application.DataTransferObject;
using IndexMarket.Domain;
using IndexMarket.Domain.Entities;

namespace IndexMarket.Application.Services;
public class ProductFactory : IProductFactory
{
    public Product MapToProduct(
        ProductForCreationDto productForCreationDto,
        Category maybeCategory,
        ProductShape productShape,
        FileModel fileModel)
    {
        return new Product
        {
            SalePrice = productForCreationDto.SalePrice,
            Status = (productForCreationDto.SalePrice == null)
                ? ProductStatus.Recommended
                : ProductStatus.Discount,
            Price = productForCreationDto.Price,
            Amount = productForCreationDto.Amount,
            Height = productForCreationDto.Height,
            Depth = productForCreationDto.Depth,
            File = fileModel,
            Category = maybeCategory,
            ProductShape = productShape
        };
    }

    public Product MapToProduct(
        ProductForCreationDtoRectangel productForCreationDtoRectangel,
        Category category,
        ProductShape productShape,
        FileModel fileModel)
    {
        return new Product
        {
            SalePrice = productForCreationDtoRectangel.SalePrice,
            Status = (productForCreationDtoRectangel.SalePrice == null) 
                ? ProductStatus.Recommended 
                : ProductStatus.Discount,
            Price = productForCreationDtoRectangel.Price,
            Amount = productForCreationDtoRectangel.Amount,
            Height = productForCreationDtoRectangel.Height,
            Weight = productForCreationDtoRectangel.Weight,
            Depth = productForCreationDtoRectangel.Depth,
            File = fileModel,
            Category = category,
            ProductShape = productShape
        };
    }

    public void MapToProduct(
        Product storageProduct,
        ProductForModificationDto productForModificationDto,
        FileModel? fileModel)
    {
        storageProduct.Price = productForModificationDto.Price ?? storageProduct.Price;
        storageProduct.Amount = productForModificationDto.Amount ?? storageProduct.Amount;
        storageProduct.Height = productForModificationDto.Height ?? storageProduct.Height;
        storageProduct.UpdatedAt = DateTime.UtcNow;

        if(storageProduct.ProductShape.Name == Shapes.Rectangle)
            storageProduct.Weight = productForModificationDto.Weight ?? storageProduct.Weight;

        if(productForModificationDto.SalePrice != null && productForModificationDto.SalePrice > 0)
            storageProduct.Status = ProductStatus.Discount;

        if (productForModificationDto.SalePrice == 0)
            storageProduct.Status = ProductStatus.Recommended;

        if(fileModel is not null)
        {
            storageProduct.File = fileModel;
        }

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

        FileDto? fileModel = default;
        if (product.File is not null)
            fileModel = new FileDto(product.File.Id, product.File.Type, product.File.FileName);

        return new ProductDto(
            productId: product.Id,
            SalePrice: product.SalePrice,
            Price: product.Price,
            Amount: product.Amount,
            Shape: productShape,
            Height: product.Height,
            Weight: product.Weight,
            Depth: product.Depth,
            File: fileModel,
            Status: Enum.GetName(typeof(ProductStatus), product.Status),
            Category: category);
    }
}
