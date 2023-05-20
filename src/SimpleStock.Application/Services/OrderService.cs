using AutoMapper;
using SimpleStock.Application.Interfaces;
using SimpleStock.Data.Interfaces;
using SimpleStock.Domain.DTOs.Order;
using SimpleStock.Domain.DTOs.OrderItem;
using SimpleStock.Domain.Enums;
using SimpleStock.Domain.Models;
using SimpleStock.Exception;

namespace SimpleStock.Application.Services;
public class OrderService : IOrderService
{
    private readonly IOrderRepository _orderRepository;
    private readonly IMapper _mapper;

    public OrderService(
        IOrderRepository orderRepository, IMapper mapper)
    {
        _orderRepository = orderRepository;
        _mapper = mapper;
    }

    public async Task<ICollection<OrderResponseDto>> GetAll()
    {
        var orders = await _orderRepository.GetAll();
        return orders
            .Select(p => _mapper.Map<OrderResponseDto>(p))
            .ToList();
    }

    public async Task<OrderResponseDto?> GetById(Guid id)
    {
        var order = await _orderRepository.GetById(id);
        if (order == null) ThrowNotFound();

        return _mapper.Map<OrderResponseDto>(order);
    }

    public async Task<OrderResponseDto?> AddOrder(OrderRequestDto request)
    {
        CheckHasDuplicatedOrderItems(request.OrderItems);
        
        var orderToPersist = _mapper.Map<OrderModel>(request);
        orderToPersist.OrderStatus = EOrderStatus.Pending;

        await _orderRepository.Add(orderToPersist);

        var order = _mapper.Map<OrderResponseDto>(orderToPersist);

        return order;
    }

    public async Task<OrderResponseDto?> UpdateOrder(Guid id, OrderRequestDto request)
    {
        var order = await _orderRepository.GetById(id);
        if (order == null) ThrowNotFound();

        _mapper.Map(request, order);

        await _orderRepository.Update(order!);

        var responseOrder = _mapper.Map<OrderResponseDto>(order);

        return responseOrder;
    }

    public async Task<bool> DeleteOrder(Guid id)
    {
        var order = await _orderRepository.GetById(id);
        if (order == null)
        {
            var message = "Não há venda cadastrada com o ID informado.";
            throw new NotFoundException(message);
        }

        return await _orderRepository.Delete(order);
    }

    private static void ThrowNotFound()
    {
        var message = "Não há venda cadastrada com o ID informado.";
        throw new NotFoundException(message);
    }

    private void CheckHasDuplicatedOrderItems(ICollection<OrderItemRequestDto> items)
    {
        var duplicatedItems = items.GroupBy(i => i.ProductId)
                                   .Where(i => i.Count() > 1)
                                   .Select(g => g.Key);

        if (duplicatedItems.Any())
        {
            var message = "Há itens de vendas duplicados na listagem de produtos.";
            throw new DuplicatedItemException(message);
        }
    }
}