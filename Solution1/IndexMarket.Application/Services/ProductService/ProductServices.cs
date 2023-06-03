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
    public ProductServices(
        AppDbContext appDbContext,
        IProductRepository productRepository,
        IProductFactory productFactory,
        ICategoryRepository categoryRepository)
    {
        this.appDbContext = appDbContext;
        this.productRepository = productRepository;
        this.productFactory = productFactory;
        this.categoryRepository = categoryRepository;
    }

    public async ValueTask<ProductDto> CreateProductAsync(ProductForCreationDto productCreationDto)
    {
        ValidateCreationProductDto(productCreationDto);

        Category? maybeCategory = await this.categoryRepository.SelectByIdAsync(productCreationDto.Category_Id);

        ValidationGeneric<Category>(maybeCategory);

        var newProduct = this.productFactory.MapToProduct(productCreationDto, maybeCategory);

        var storageProduct = await this.productRepository.InsertAsync(newProduct);

        return this.productFactory.MapToProductDto(storageProduct);
    }

    public ValueTask<ProductDto> CreateRectangelProductAsync(ProductForCreationDtoRectangel productForCreationDtoRectangel)
    {
        throw new NotImplementedException();
    }

    public IQueryable<ProductDto> RetrieveProducts()
    {
        var products = this.productRepository.SelectAll();

        return products.Select(x => this.productFactory.MapToProductDto(x));
    }

    public async ValueTask<ProductDto> RetrieveProductByIdAsync(Guid productId)
    {
        ValidationProductId(productId);

        var storageProduct = await this.productRepository
            .SelectByIdWithDetailsAsync(
            expression: pro => pro.Id == productId,
            includes: new string[] {nameof(Product.Category), nameof(Product.ProductShape)});

        ValidationStorageProduct(storageProduct, productId);

        return this.productFactory.MapToProductDto(storageProduct);
    }

    public ValueTask<ProductDto> ModifyProductAsync(ProductForModificationDto productForModificationDto)
    {
        throw new NotImplementedException();
    }

    public async ValueTask<ProductDto> RemoveProductAsync(Guid productId)
    {
        ValidationProductId(productId);

        var product = await this.productRepository.SelectByIdAsync(productId);

        ValidationStorageProduct(product, productId);

        var removePro = await this.productRepository.DeleteAsync(product);

        return this.productFactory.MapToProductDto(removePro);
    }
}
