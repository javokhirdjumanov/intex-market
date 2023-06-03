using IndexMarket.Application.DataTransferObject;
using IndexMarket.Domain.Entities;
using IndexMarket.Infrastructure.Context;
using IndexMarket.Infrastructure.Repository;

namespace IndexMarket.Application.Services;
public partial class ProductServices : IProductServices
{
    private readonly AppDbContext appDbContext;
    private readonly IProductRepository productRepository;
    private readonly IProductFactory productFactory;
    private readonly ICategoryRepository categoryRepository;
    private readonly IProductShapeRepository productShapeRepository;
    public ProductServices(
        AppDbContext appDbContext,
        IProductRepository productRepository,
        IProductFactory productFactory,
        ICategoryRepository categoryRepository,
        IProductShapeRepository productShapeRepository)
    {
        this.appDbContext = appDbContext;
        this.productRepository = productRepository;
        this.productFactory = productFactory;
        this.categoryRepository = categoryRepository;
        this.productShapeRepository = productShapeRepository;
    }

    public async ValueTask<ProductDto> CreateCircleProductAsync(ProductForCreationDto productCreationDto)
    {
        ValidateCreationProductDto(productCreationDto);

        Category? maybeCategory = await this.categoryRepository
            .GetCategoryByNameAsync(productCreationDto.category);

        ProductShape? maybeProductShape = await this.productShapeRepository
            .GetShapeByNameAsync(productCreationDto.shape);

        var newProduct = this.productFactory.MapToProduct(productCreationDto, maybeCategory, maybeProductShape);

        var storageProduct = await this.productRepository.InsertAsync(newProduct);


        return this.productFactory.MapToProductDto(storageProduct);

    }

    public ValueTask<ProductDto> CreateRectangelProductAsync(ProductForCreationDtoRectangel productForCreationDtoRectangel)
    {
        throw new NotImplementedException();
    }

    public IQueryable<ProductDto> RetrieveProducts()
    {
        throw new NotImplementedException();
    }

    public ValueTask<ProductDto> RetrieveProductByIdAsync(Guid productId)
    {
        throw new NotImplementedException();
    }

    public ValueTask<ProductDto> ModifyProductAsync(ProductForModificationDto productForModificationDto)
    {
        throw new NotImplementedException();
    }

    public ValueTask<ProductDto> RemoveProductAsync(Guid productId)
    {
        throw new NotImplementedException();
    }
}
