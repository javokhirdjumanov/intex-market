using IndexMarket.Application.DataTransferObject;
using IndexMarket.Application.Paginations;

namespace IndexMarket.Application.Services;
public interface ICategoryServices
{
    ValueTask<CategoryDto> CreateCategoryAysnc(string categoryName);
    IQueryable<CategoryDto> RetrieveCategories(QueryParametrs queryParametrs);
    ValueTask<CategoryDtoWithProducts> RetrieveCategoryByIdWithProductsAsync(Guid categoryId);
    ValueTask<CategoryDto> ModifyCategoryAsync(CategoryModifyDto categoryModifyDto);
    ValueTask<CategoryDto> RemoveCategoryAsync(Guid categoryId);
}
