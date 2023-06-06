using IndexMarket.Application.DataTransferObject;
using IndexMarket.Application.Extantions;
using IndexMarket.Application.Paginations;
using IndexMarket.Domain;
using IndexMarket.Domain.Entities;
using IndexMarket.Infrastructure.Context;
using IndexMarket.Infrastructure.Repository;
using Microsoft.AspNetCore.Http;

namespace IndexMarket.Application.Services;
public partial class ProductServices : IProductServices
{
    private readonly AppDbContext appDbContext;
    private readonly IProductRepository productRepository;
    private readonly IProductFactory productFactory;
    private readonly ICategoryRepository categoryRepository;
    private readonly IProductShapeRepository productShapeRepository;
    private readonly IFileRepository fileRepository;
    private readonly IHttpContextAccessor httpContextAccessor;
    public ProductServices(
        AppDbContext appDbContext,
        IProductRepository productRepository,
        IProductFactory productFactory,
        ICategoryRepository categoryRepository,
        IProductShapeRepository productShapeRepository,
        IFileRepository fileRepository,
        IHttpContextAccessor httpContextAccessor)
    {
        this.appDbContext = appDbContext;
        this.productRepository = productRepository;
        this.productFactory = productFactory;
        this.categoryRepository = categoryRepository;
        this.productShapeRepository = productShapeRepository;
        this.fileRepository = fileRepository;
        this.httpContextAccessor = httpContextAccessor;
    }

    public async ValueTask<ProductDto> CreateProductAsync(ProductForCreationDto productCreationDto)
    {
        ValidateCreationProductDto(productCreationDto);

        /// <summary>
        /// Categories
        productCreationDto.Category_Id.IsDefault();

        Category? maybeCategory = await this.categoryRepository
            .SelectByIdAsync(productCreationDto.Category_Id);

        ValidationStorageObject
            .ValidationGeneric<Category>(maybeCategory, productCreationDto.Category_Id);
        /// </summary>

        /// Product Shape
        productCreationDto.Shape_Id.IsDefault();

        ProductShape? productShape = await this.productShapeRepository
            .SelectByIdAsync(productCreationDto.Shape_Id);

        ValidationStorageObject
            .ValidationGeneric<ProductShape>(productShape, productCreationDto.Category_Id);
        /// </summary>

        ValidationNotRectangel(productShape);

        /// Product Files
        productCreationDto.File_Id.IsDefault();

        var storageProductPhoto = await this.fileRepository
            .GetFileByIdAsync(productCreationDto.File_Id);

        ValidationStorageObject
            .ValidationGeneric<FileModel>(storageProductPhoto, productCreationDto.Category_Id);
        /// </summary>

        var newProduct = this.productFactory
            .MapToProduct(productCreationDto, maybeCategory, productShape, storageProductPhoto);

        var storageProduct = await this.productRepository
            .InsertAsync(newProduct);

        return this.productFactory
            .MapToProductDto(storageProduct);
    }

    public async ValueTask<ProductDto> CreateRectangelProductAsync(
        ProductForCreationDtoRectangel productForCreationDtoRectangel)
    {
        ValidationCreationRectangleProductDto(productForCreationDtoRectangel);

        /// Categories
        productForCreationDtoRectangel.Category_Id.IsDefault();

        Category? maybeCategory = await this.categoryRepository
            .SelectByIdAsync(productForCreationDtoRectangel.Category_Id);

        ValidationStorageObject
            .ValidationGeneric<Category>(maybeCategory, productForCreationDtoRectangel.Category_Id);
        /// </summary>

        /// Product Shape
        productForCreationDtoRectangel.Shape_Id.IsDefault();

        ProductShape? maybeShape = await this.productShapeRepository
            .SelectByIdAsync(productForCreationDtoRectangel.Shape_Id);

        ValidationStorageObject
            .ValidationGeneric<ProductShape>(maybeShape, productForCreationDtoRectangel.Shape_Id);
        /// </summary>
        
        ValidationRectangel(maybeShape);

        /// Product Files
        productForCreationDtoRectangel.File_Id.IsDefault();

        var storageProductPhoto = await this.fileRepository
            .GetFileByIdAsync(productForCreationDtoRectangel.File_Id);

        ValidationStorageObject
             .ValidationGeneric<FileModel>(storageProductPhoto,
                                           productForCreationDtoRectangel.Category_Id);
        /// </summary>
        
        var newProduct = this.productFactory
            .MapToProduct(
            productForCreationDtoRectangel,
            maybeCategory,
            maybeShape,
            storageProductPhoto);

        var storageProduct = await this.productRepository
            .InsertAsync(newProduct);

        return this.productFactory
            .MapToProductDto(storageProduct);
    }

    public IQueryable<ProductDto> RetrieveProducts(QueryParametrs queryParametrs)
    {
        var products = this.productRepository
            .SelectAll()
            .ToPagedList(
                httpContext: this.httpContextAccessor.HttpContext,
                pageSize: queryParametrs.Page.Size,
                pageIndex: queryParametrs.Page.Index);

        return products
            .Select(x => this.productFactory
            .MapToProductDto(x));
    }

    public async ValueTask<ProductDto> RetrieveProductByIdAsync(Guid productId)
    {
        productId.IsDefault();

        var storageProduct = await this.productRepository
            .SelectByIdWithDetailsAsync(
            expression: pro => pro.Id == productId,
            includes: new string[]
            {
                nameof(Product.Category),
                nameof(Product.ProductShape),
                nameof(Product.File)
            });

        ValidationStorageObject
            .ValidationGeneric<Product>(storageProduct, productId);

        return this.productFactory
            .MapToProductDto(storageProduct);
    }

    public async ValueTask<ProductDto> ModifyProductAsync(
        ProductForModificationDto productForModificationDto)
    {
        ValidateModificationProductDto(productForModificationDto);

        productForModificationDto.Product_Id.IsDefault();

        Product? storageProduct = await this.productRepository
            .SelectByIdWithDetailsAsync(
            expression: p => p.Id == productForModificationDto.Product_Id,
            includes: new string[]
            {
                nameof(Product.Category),
                nameof(Product.ProductShape),
                nameof(Product.File)
            });

        ValidationStorageObject
            .ValidationGeneric<Product>(storageProduct, productForModificationDto.Product_Id);

        FileModel? fileModel = default;
        if (productForModificationDto.Photo_Id.HasValue)
        {
            Guid guid = productForModificationDto.Photo_Id.Value;
            fileModel = await this.fileRepository.GetFileByIdAsync(guid);
        }

        this.productFactory
            .MapToProduct(storageProduct, productForModificationDto, fileModel);

        var modifyProduct = await this.productRepository
            .UpdateAsync(storageProduct);

        return this.productFactory
            .MapToProductDto(modifyProduct);
    }

    public async ValueTask<ProductDto> RemoveProductAsync(Guid productId)
    {
        productId.IsDefault();

        var storageProduct = await this.productRepository
            .SelectByIdAsync(productId);

        ValidationStorageObject
            .ValidationGeneric<Product>(storageProduct, productId);

        storageProduct.Status = ProductStatus.Deleted;

        return this.productFactory
            .MapToProductDto(storageProduct);
    }
}
