using IndexMarket.Application.DataTransferObject;
using IndexMarket.Domain.Entities;
using IndexMarket.Infrastructure.Context;
using IndexMarket.Infrastructure.Repository;
using System.Diagnostics;
using System.Text.Json;

namespace IndexMarket.Application.Services;
public partial class CategoryServices : ICategoryServices
{
    private readonly ICategoryRepository categoryRepository;
    private readonly AppDbContext context;
    public CategoryServices(ICategoryRepository categoryRepository, AppDbContext context)
    {
        this.categoryRepository = categoryRepository;
        this.context = context;
    }

    public async ValueTask<CategoryDto> CreateCategoryAysnc(string categoryName)
    {
        ValidationCategoryName(categoryName);

        var addedCategory = await this.categoryRepository.InsertAsync(new Category { Title = categoryName });

        return new CategoryDto(addedCategory.Id, addedCategory.Title);
    }

    public IQueryable<CategoryDto> RetrieveCategories()
    {
        var categories = this.categoryRepository.SelectAll();

        return categories.Select(x => new CategoryDto(x.Id, x.Title));
    }

    public async ValueTask<CategoryDto> RetrieveCategoryByIdAsync(Guid categoryId)
    {
        ValidationCategoryId(categoryId);

        var storageCategory = await this.categoryRepository.SelectByIdAsync(categoryId);

        ValidationStorageCategory(storageCategory, categoryId);

        return new CategoryDto(storageCategory.Id, storageCategory.Title);
    }

    public async ValueTask<CategoryDto> ModifyCategoryAsync(CategoryModifyDto categoryModifyDto)
    {
        ValidationCategoryForModify(categoryModifyDto);

        var storageCategory = await this.categoryRepository.SelectByIdAsync(categoryModifyDto.id);

        ValidationStorageCategory(storageCategory, categoryModifyDto.id);

        storageCategory.Title = categoryModifyDto.Title ?? storageCategory.Title;

        var updateCategory = await this.categoryRepository.UpdateAsync(storageCategory);

        return new CategoryDto(updateCategory.Id, updateCategory.Title);
    }

    public async ValueTask<CategoryDto> RemoveCategoryAsync(Guid categoryId)
    {
        ValidationCategoryId(categoryId);

        var storageCategory = await this.categoryRepository.SelectByIdAsync(categoryId);

        ValidationStorageCategory(storageCategory, categoryId);

        var removedCategory = await this.categoryRepository.DeleteAsync(storageCategory);

        return new CategoryDto(removedCategory.Id, removedCategory.Title);
    }
}
