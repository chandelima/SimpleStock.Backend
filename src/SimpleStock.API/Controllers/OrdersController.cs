using Microsoft.AspNetCore.Mvc;
using SimpleStock.API.Shared;
using SimpleStock.Application.Interfaces;
using SimpleStock.Domain.DTOs.Order;
using SimpleStock.Domain.DTOs.OrderItem;

namespace SimpleStock.API.Controllers;
[Route("api/[controller]")]
[ApiController]
public class OrdersController : ControllerBase
{
    private readonly IOrderService _orderService;

    public OrdersController(
        IOrderService orderService)
    {
        _orderService = orderService;
    }

    [HttpGet]
    public async Task<ActionResult<ICollection<OrderResponseDto>>> GetAll()
    {
        var order = await _orderService.GetAll();
        return Ok(order);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<OrderResponseDto>> GetById(
        [FromRoute] Guid id)
    {
        var order = await _orderService.GetById(id);
        return Ok(order);
    }

    [HttpPost]
    public async Task<ActionResult<OrderResponseDto>> CreateOrder(
        [FromBody] OrderRequestDto request)
    {
        var order = await _orderService.AddOrder(request);
        return Ok(order);
    }

    [HttpPost("process/{id}")]
    public async Task<ActionResult> ProcessOrder([FromRoute] Guid id)
    {
        await _orderService.ProcessOrder(id);
        return Ok();
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<OrderResponseDto>> UpdateOrder(
        [FromRoute] Guid id, [FromBody] OrderRequestDto request)
    {
        var order = await _orderService.UpdateOrder(id, request);
        if (order == null)
        {
            var message = "Erro ao tentar atualizar produto.";
            return BadRequest(message);
        }

        return Ok(order);
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteOrder([FromRoute] Guid id)
    {
        var result = await _orderService.DeleteOrder(id);

        if (!result)
        {
            var message = "Não foi possível deletar o produto especificado.";
            return BadRequest(new MessageResponse(message));
        }

        return NoContent();
    }
}
