using IndexMarket.Application.DataTransferObject;
using IndexMarket.Infrastructure.Context;
using IndexMarket.Infrastructure.Repository;

namespace IndexMarket.Application.Services;
public partial class ProductServices : IProductServices
{
    private readonly AppDbContext appDbContext;
    private readonly IProductRepository productRepository;
    private readonly IProductFactory productFactory;
    public ProductServices(
        AppDbContext appDbContext,
        IProductRepository productRepository,
        IProductFactory productFactory)
    {
        this.appDbContext = appDbContext;
        this.productRepository = productRepository;
        this.productFactory = productFactory;
    }

    public async ValueTask<ProductDto> CreateProductAsync(ProductForCreationDto productCreationDto)
    {
        ValidateCreationProductDto(productCreationDto);

        var newProduct = this.productFactory.MapToProduct(productCreationDto);

        var addedProduct = await this.productRepository.InsertAsync(newProduct);

        return this.productFactory.MapToProductDto(addedProduct);
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
