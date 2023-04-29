using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using SimpleStock.API.Shared;
using SimpleStock.Application.Interfaces;
using SimpleStock.Domain.DTOs.Product;

namespace SimpleStock.API.Controllers;
[Route("api/products")]
[ApiController]
public class ProductController : ControllerBase
{
    private readonly IProductService _productService;

    public ProductController(
        IProductService productService)
    {
        _productService = productService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var product = await _productService.GetAll();
        return Ok(product);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById([FromRoute] Guid id)
    {
        var product = await _productService.GetById(id);
        return Ok(product);
    }

    [HttpPost]
    public async Task<IActionResult> CreateProduct(
        [FromBody] ProductRequestDto request)
    {
        var product = await _productService.AddProduct(request);
        return Ok(product);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateProduct(
        [FromRoute] Guid id, [FromBody] ProductRequestDto request)
    {
        var product = await _productService.UpdateProduct(id, request);
        if (product == null)
            return BadRequest("Erro ao tentar atualizar produto.");

        return Ok(product);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteProduct([FromRoute] Guid id)
    {
        var result = await _productService.DeleteProduct(id);

        if (!result)
        {
            var message = new MessageResponse("Não foi possível deletar o produto especificado.");
            return BadRequest(message);
        }

        return NoContent();
    }
}
