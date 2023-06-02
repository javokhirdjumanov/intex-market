using IndexMarket.Application.DataTransferObject;
using IndexMarket.Application.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace IndexMarket.Api.Controllers;
[Route("api/[controller]")]
[ApiController]
public class ProductController : ControllerBase
{
    private readonly IProductServices productServices;
    public ProductController(IProductServices productServices)
    {
        this.productServices = productServices;
    }

    [HttpPost]
    public async ValueTask<ActionResult<ProductDto>> PostProductAsync(
        ProductForCreationDto productForCreation)
    {
        var createProduct = await this.productServices
            .CreateProductAsync(productForCreation);

        return Created("", createProduct);
    }
}
