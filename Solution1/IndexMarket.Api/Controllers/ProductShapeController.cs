using IndexMarket.Application.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace IndexMarket.Api.Controllers;
[Route("api/[controller]")]
[ApiController]
public class ProductShapeController : ControllerBase
{
    private readonly IProductShapeService productShapeService;

    public ProductShapeController(IProductShapeService productShapeService)
    {
        this.productShapeService = productShapeService;
    }

    [HttpGet("All")]
    public IActionResult GetProductShapes()
    {
        var productShapes = productShapeService.RetrieveProductsShapes();

        return Ok(productShapes);
    }
}
