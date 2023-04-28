using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using SimpleStock.Application.Interfaces;
using SimpleStock.Domain.DTOs.Product;

namespace SimpleStock.API.Controllers;
[Route("api/products")]
[ApiController]
public class ProductController : ControllerBase
{
    private readonly IProductService _productService;

    public ProductController(
        IActionContextAccessor actionContextAccessor, 
        IProductService productService)
    {
        _productService = productService;
    }

    [HttpPost]
    public async Task<IActionResult> Create(
        [FromBody] ProductInputModel request)
    {
        var product = await _productService.AddProduct(request);

        Model

        return Ok(product);
    }
}
