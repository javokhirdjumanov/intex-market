using IndexMarket.Application.DataTransferObject;
using IndexMarket.Application.Extantions;
using IndexMarket.Application.Paginations;
using IndexMarket.Domain;
using IndexMarket.Domain.Entities;
using IndexMarket.Infrastructure.Context;
using IndexMarket.Infrastructure.Repository;
using Microsoft.AspNetCore.Http;

namespace IndexMarket.Application.Services;
public partial class CategoryServices : ICategoryServices
{
    private readonly IProductShapeRepository productShapeRepository;
    private readonly ICategoryRepository categoryRepository;
    private readonly AppDbContext context;
    private readonly IFileRepository fileRepository;
    private readonly IHttpContextAccessor httpContextAccessor;
    public CategoryServices(
        ICategoryRepository categoryRepository,
        AppDbContext context,
        IProductShapeRepository productShapeRepository,
        IFileRepository fileRepository,
        IHttpContextAccessor httpContextAccessor)
    {
        this.categoryRepository = categoryRepository;
        this.context = context;
        this.productShapeRepository = productShapeRepository;
        this.fileRepository = fileRepository;
        this.httpContextAccessor = httpContextAccessor;
    }

    public async ValueTask<CategoryDto> CreateCategoryAysnc(string categoryName)
    {
        var addedCategory = await this.categoryRepository
            .InsertAsync(new Category
            {
                Title = categoryName
            });

        return new CategoryDto(addedCategory.Id, addedCategory.Title); ;
    }

    public IQueryable<CategoryDto> RetrieveCategories(QueryParametrs queryParametrs)
    {
        var categories = this.categoryRepository
            .SelectAll()
            .ToPagedList(
                httpContext: this.httpContextAccessor.HttpContext,
                pageSize: queryParametrs.Page.Size,
                pageIndex: queryParametrs.Page.Index);

        return categories
            .Select(x => new CategoryDto(x.Id, x.Title));
    }

    public async ValueTask<CategoryDtoWithProducts> RetrieveCategoryByIdWithProductsAsync(Guid categoryId)
    {
        categoryId.IsDefault();

        var storageCategory = await this.categoryRepository
            .SelectByIdWithDetailsAsync(
            expression: category => category.Id == categoryId,
            includes: new string[]
            {
                nameof(Category.Products)
            });

        var products = storageCategory.Products
            .Select(p => p)
            .ToList();

        ValidationStorageObject
            .ValidationGeneric<Category>(storageCategory, categoryId);

        foreach (var item in products)
        {
            var shape = await this.productShapeRepository
                .SelectByIdAsync(item.Shape_Id);

            var file = await this.fileRepository
                .GetFileByIdAsync(item.File_Id);

            item.File = file;
            item.ProductShape = shape;
        }

        var productDtos = new List<ProductDto>(
            products.Select(x => new ProductDto(
                x.Id,
                x.SalePrice,
                x.Price,
                x.Amount,
                new ProductShapeDto(x.ProductShape.Id, x.ProductShape.Name),
                x.Height,
                x.Weight,
                x.Depth,
                new FileDto(x.File.Id, x.File.Type, x.File.FileName),
                Enum.GetName(typeof(ProductStatus), x.Status),
                new CategoryDto(x.Category.Id, x.Category.Title)))
            );

        return new CategoryDtoWithProducts(productDtos);
    }

    public async ValueTask<CategoryDto> ModifyCategoryAsync(CategoryModifyDto categoryModifyDto)
    {
        ValidationCategoryForModify(categoryModifyDto);

        var storageCategory = await this.categoryRepository
            .SelectByIdAsync(categoryModifyDto.id);

        ValidationStorageObject
            .ValidationGeneric<Category>(storageCategory, categoryModifyDto.id);

        storageCategory.Title = categoryModifyDto.Title ?? storageCategory.Title;
        storageCategory.UpdatedAt = DateTime.UtcNow;

        var updateCategory = await this.categoryRepository
            .UpdateAsync(storageCategory);

        return new CategoryDto(updateCategory.Id, updateCategory.Title);
    }

    public async ValueTask<CategoryDto> RemoveCategoryAsync(Guid categoryId)
    {
        categoryId.IsDefault();

        var storageCategory = await this.categoryRepository
            .SelectByIdAsync(categoryId);

        ValidationStorageObject
            .ValidationGeneric<Category>(storageCategory, categoryId);

        var removedCategory = await this.categoryRepository
            .DeleteAsync(storageCategory);

        return new CategoryDto(removedCategory.Id, removedCategory.Title);
    }
}
