using IndexMarket.Application.DataTransferObject;
using IndexMarket.Application.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace IndexMarket.Api.Controllers;
[Route("api/[controller]")]
[ApiController]
public class ProductController : ControllerBase
{
    private readonly IProductServices productServices;
    public ProductController(
        IProductServices productServices)
    {
        this.productServices = productServices;
    }

    [AllowAnonymous]
    [HttpPost]
    public async ValueTask<ActionResult<ProductDto>> PostProductAsync(ProductForCreationDto productForCreationDto)
    {
        var product = await this.productServices.CreateProductAsync(productForCreationDto);

        return Created("", product);
    }

    [HttpPost("Rectangel")]
    public async ValueTask<ActionResult<ProductDto>> PostProductRectangelAsync(
        ProductForCreationDtoRectangel productForCreationDtoRectangel)
    {
        var product = await this.productServices.CreateRectangelProductAsync(productForCreationDtoRectangel);

        return Created("", product); 
    }

    [HttpGet("All")]
    public IActionResult GetAllProducts()
    {
        var products = this.productServices.RetrieveProducts();

        return Ok(products);
    }

    [HttpGet("{productId:guid}")]
    public async ValueTask<ActionResult<ProductDto>> GetProductByIdAsync(Guid productId)
    {
        var product = await this.productServices.RetrieveProductByIdAsync(productId);

        return Ok(product);
    }

    [HttpPut]
    public async ValueTask<ActionResult<ProductDto>> PutProductAsync(ProductForModificationDto productForModificationDto)
    {
        var modifyProduct = await this.productServices.ModifyProductAsync(productForModificationDto);

        return Ok(modifyProduct);
    }

    [HttpDelete("{productId:guid}")]
    public async ValueTask<ActionResult<ProductDto>> DeleteProductAsync(Guid productId)
    {
        var removeProduct = await this.productServices.RemoveProductAsync(productId);

        return Ok(removeProduct);
    }
}
