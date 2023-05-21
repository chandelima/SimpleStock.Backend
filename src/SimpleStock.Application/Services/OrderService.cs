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
    private readonly IOrderItemService _orderItemService;
    private readonly IOrderRepository _orderRepository;
    private readonly IMapper _mapper;

    public OrderService(
        IOrderItemService orderItemService,
        IOrderRepository orderRepository,
        IMapper mapper)
    {
        _orderItemService = orderItemService;
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
        _orderItemService.CheckHasDuplicatedOrderItems(request.OrderItems);
        
        var orderToPersist = _mapper.Map<OrderModel>(request);
        orderToPersist.OrderStatus = EOrderStatus.Pending;
        orderToPersist.OrderItems = _orderItemService.ProcessOrderItemsPrices(request.OrderItems);

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
}