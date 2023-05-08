using Microsoft.AspNetCore.Mvc;
using SimpleStock.API.Shared;
using SimpleStock.Application.Interfaces;
using SimpleStock.Domain.DTOs.Customer;

namespace SimpleStock.API.Controllers;
[Route("api/[controller]")]
[ApiController]
public class CustomersController : ControllerBase
{
    private readonly ICustomerService _customerService;

    public CustomersController(
        ICustomerService customerService)
    {
        _customerService = customerService;
    }

    [HttpGet]
    public async Task<ActionResult<ICollection<CustomerResponseDto>>> GetAll()
    {
        var customer = await _customerService.GetAll();
        return Ok(customer);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<CustomerResponseDto>> GetById(
        [FromRoute] Guid id)
    {
        var customer = await _customerService.GetById(id);
        return Ok(customer);
    }

    [HttpPost]
    public async Task<ActionResult<CustomerResponseDto>> CreateCustomer(
        [FromBody] CustomerRequestDto request)
    {
        var customer = await _customerService.AddCustomer(request);
        return Ok(customer);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<CustomerResponseDto>> UpdateCustomer(
        [FromRoute] Guid id, [FromBody] CustomerRequestDto request)
    {
        var customer = await _customerService.UpdateCustomer(id, request);
        if (customer == null)
        {
            var message = "Erro ao tentar atualizar produto.";
            return BadRequest(message);
        }

        return Ok(customer);
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteCustomer([FromRoute] Guid id)
    {
        var result = await _customerService.DeleteCustomer(id);

        if (!result)
        {
            var message = "Não foi possível deletar o produto especificado.";
            return BadRequest(new MessageResponse(message));
        }

        return NoContent();
    }
}
