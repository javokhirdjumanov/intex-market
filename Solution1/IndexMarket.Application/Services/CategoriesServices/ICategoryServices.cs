using IndexMarket.Application.DataTransferObject;

namespace IndexMarket.Application.Services;
public interface ICategoryServices
{
    ValueTask<CategoryDto> CreateCategoryAysnc(string categoryName);
    IQueryable<CategoryDto> RetrieveCategories();
    ValueTask<CategoryDto> RetrieveCategoryByIdAsync(Guid categoryId);
    ValueTask<CategoryDto> ModifyCategoryAsync(CategoryModifyDto categoryModifyDto);
    ValueTask<CategoryDto> RemoveCategoryAsync(Guid categoryId);
}
