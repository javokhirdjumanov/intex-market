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
    private readonly IFileRepository fileRepository;
    public ProductServices(
        AppDbContext appDbContext,
        IProductRepository productRepository,
        IProductFactory productFactory,
        ICategoryRepository categoryRepository,
        IProductShapeRepository productShapeRepository,
        IFileRepository fileRepository)
    {
        this.appDbContext = appDbContext;
        this.productRepository = productRepository;
        this.productFactory = productFactory;
        this.categoryRepository = categoryRepository;
        this.productShapeRepository = productShapeRepository;
        this.fileRepository = fileRepository;
    }

    public async ValueTask<ProductDto> CreateProductAsync(ProductForCreationDto productCreationDto)
    {
        ValidateCreationProductDto(productCreationDto);

        ValidationId(productCreationDto.Category_Id);
        Category? maybeCategory = await this.categoryRepository.SelectByIdAsync(productCreationDto.Category_Id);
        ValidationGeneric<Category>(maybeCategory);

        ValidationId(productCreationDto.Shape_Id);
        ProductShape? productShape = await this.productShapeRepository.SelectByIdAsync(productCreationDto.Shape_Id);
        ValidationGeneric<ProductShape>(productShape);
        ValidationNotRectangel(productShape);

        ValidationId(productCreationDto.File_Id);
        var storageProductPhoto = await this.fileRepository.GetFileByIdAsync(productCreationDto.File_Id);
        ValidationGeneric<FileModel>(storageProductPhoto);

        var newProduct = this.productFactory
            .MapToProduct(productCreationDto, maybeCategory, productShape, storageProductPhoto);

        var storageProduct = await this.productRepository.InsertAsync(newProduct);

        return this.productFactory.MapToProductDto(storageProduct);
    }

    public async ValueTask<ProductDto> CreateRectangelProductAsync(
        ProductForCreationDtoRectangel productForCreationDtoRectangel)
    {
        ValidationCreationRectangleProductDto(productForCreationDtoRectangel);

        ValidationId(productForCreationDtoRectangel.Category_Id);
        Category? maybeCategory = await this.categoryRepository
            .SelectByIdAsync(productForCreationDtoRectangel.Category_Id);
        ValidationGeneric<Category>(maybeCategory);

        ValidationId(productForCreationDtoRectangel.Shape_Id);
        ProductShape? maybeShape = await this.productShapeRepository
        .SelectByIdAsync(productForCreationDtoRectangel.Shape_Id);
        ValidationGeneric<ProductShape>(maybeShape);

        ValidationRectangel(maybeShape);

        ValidationId(productForCreationDtoRectangel.File_Id);
        var storageProductPhoto = await this.fileRepository.GetFileByIdAsync(productForCreationDtoRectangel.File_Id);
        ValidationGeneric<FileModel>(storageProductPhoto);

        var newProduct = this.productFactory
            .MapToProduct(productForCreationDtoRectangel, maybeCategory, maybeShape, storageProductPhoto);

        var storageProduct = await this.productRepository.InsertAsync(newProduct);

        return this.productFactory.MapToProductDto(storageProduct);
    }

    public IQueryable<ProductDto> RetrieveProducts()
    {
        var products = this.productRepository.SelectAll();

        return products.Select(x => this.productFactory.MapToProductDto(x));
    }

    public async ValueTask<ProductDto> RetrieveProductByIdAsync(Guid productId)
    {
        ValidationId(productId);

        var storageProduct = await this.productRepository
            .SelectByIdWithDetailsAsync(
            expression: pro => pro.Id == productId,
            includes: new string[] { nameof(Product.Category), nameof(Product.ProductShape), nameof(Product.File) });

        ValidationStorageProduct(storageProduct, productId);

        return this.productFactory.MapToProductDto(storageProduct);
    }

    public async ValueTask<ProductDto> ModifyProductAsync(ProductForModificationDto productForModificationDto)
    {
        ValidateModificationProductDto(productForModificationDto);

        ValidationId(productForModificationDto.Product_Id);

        Product? storageProduct = await this.productRepository.SelectByIdWithDetailsAsync(
            expression: p => p.Id == productForModificationDto.Product_Id,
            includes: new string[] { nameof(Product.Category), nameof(Product.ProductShape), nameof(Product.File) });

        ValidationStorageProduct(storageProduct, productForModificationDto.Product_Id);

        this.productFactory.MapToProduct(storageProduct, productForModificationDto);

        var modifyProduct = await this.productRepository.UpdateAsync(storageProduct);

        return this.productFactory.MapToProductDto(modifyProduct);
    }

    public async ValueTask<ProductDto> RemoveProductAsync(Guid productId)
    {
        ValidationId(productId);

        var product = await this.productRepository.SelectByIdAsync(productId);

        ValidationStorageProduct(product, productId);

        var removePro = await this.productRepository.DeleteAsync(product);

        return this.productFactory.MapToProductDto(removePro);
    }
}
