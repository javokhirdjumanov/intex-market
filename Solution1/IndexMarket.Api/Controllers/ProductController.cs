using IndexMarket.Application.Services;
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



}
