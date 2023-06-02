using IndexMarket.Application.DataTransferObject;
using IndexMarket.Application.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace IndexMarket.Api.Controllers;
[Route("api/[controller]")]
[ApiController]
public class CategoryController : ControllerBase
{
    private readonly ICategoryServices categoryServices;
    public CategoryController(ICategoryServices categoryServices)
    {
        this.categoryServices = categoryServices;
    }

    [AllowAnonymous]
    [HttpPost]
    public async ValueTask<ActionResult<CategoryDto>> PostCategoryAsync(string categoryName)
    {
        var newCategory = await this.categoryServices.CreateCategoryAysnc(categoryName);

        return Created("", newCategory);
    }

    [HttpGet]
    public IActionResult GetCategories()
    {
        var categories = this.categoryServices.RetrieveCategories();

        return Ok(categories);
    }

    [HttpGet("{categoryId:guid}")]
    public async ValueTask<ActionResult<CategoryDto>> GetCategoryByIdAsync(Guid categoryId)
    {
        var category = await this.categoryServices.RetrieveCategoryByIdAsync(categoryId);

        return Ok(category);
    }

    [HttpPut]
    public async ValueTask<ActionResult<CategoryDto>> PutCategoryAsync(CategoryModifyDto categoryModifyDto)
    {
        var modifyCategory = await this.categoryServices.ModifyCategoryAsync(categoryModifyDto);

        return Ok(modifyCategory);
    }

    [HttpDelete("{categoryId:guid}")]
    public async ValueTask<ActionResult<CategoryDto>> DeleteCategoryAsync(Guid categoryId)
    {
        var removeCategory = await this.categoryServices.RemoveCategoryAsync(categoryId);

        return Ok(removeCategory);
    }
}