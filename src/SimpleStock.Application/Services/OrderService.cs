using AutoMapper;
using SimpleStock.Application.Interfaces;
using SimpleStock.Data.Interfaces;
using SimpleStock.Domain.DTOs.Order;
using SimpleStock.Domain.Enums;
using SimpleStock.Domain.Models;
using SimpleStock.Exception;

namespace SimpleStock.Application.Services;
public class OrderService : IOrderService
{
    private readonly ICustomerService _customerService;
    private readonly IOrderItemService _orderItemService;
    private readonly IOrderRepository _orderRepository;
    private readonly IMapper _mapper;

    public OrderService(
        ICustomerService customerService,
        IOrderItemService orderItemService,
        IOrderRepository orderRepository,
        IMapper mapper)
    {
        _customerService = customerService;
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

    public async Task<OrderResponseDto> GetById(Guid id)
    {
        var order = await _orderRepository.GetById(id);
        if (order == null) ThrowNotFound();

        return _mapper.Map<OrderResponseDto>(order);
    }

    public async Task<OrderResponseDto?> AddOrder(OrderRequestDto request)
    {
        await _customerService.GetById(request.CustomerId);

        var orderToPersist = _mapper.Map<OrderModel>(request);
        orderToPersist.OrderStatus = EOrderStatus.Pending;
        orderToPersist.OrderItems = await _orderItemService
            .ProcessOrderItems(request.OrderItems);

        await _orderRepository.Add(orderToPersist);

        var order = _mapper.Map<OrderResponseDto>(orderToPersist);

        return order;
    }

    public async Task<OrderResponseDto?> UpdateOrder(Guid id,OrderRequestDto request)
    {
        var order = await _orderRepository.GetById(id);
        if (order == null) ThrowNotFound();
        if (order!.OrderStatus != EOrderStatus.Pending)
        {
            var message = "O status da operação de venda não permite alteração";
            throw new NotAllowedException(message);
        }

        await _customerService.GetById(request.CustomerId);

        _mapper.Map(request, order);
        order.OrderItems = await _orderItemService.ProcessOrderItems(request.OrderItems);

        await _orderRepository.Update(order!);

        var responseOrder = _mapper.Map<OrderResponseDto>(order);

        return responseOrder;
    }

    public async Task<bool> DeleteOrder(Guid id)
    {
        var order = await _orderRepository.GetById(id);
        if (order == null) ThrowNotFound();
        if (order!.OrderStatus != EOrderStatus.Pending)
        {
            var message = "O status da operação de venda não permite alteração";
            throw new NotAllowedException(message);
        }

        return await _orderRepository.Delete(order!);
    }

    public async Task ProcessOrder(Guid id)
    {
        var order = await _orderRepository.GetById(id);
        if (order == null) ThrowNotFound();

        if (order!.OrderStatus != EOrderStatus.Pending)
        {
            var message = "O status da operação de venda não permite a alteração";
            throw new NotAllowedException(message);
        }

        await _orderItemService.DecreaseOrdemItemsStock(order.OrderItems);
        

        order.OrderStatus = EOrderStatus.Completed;
        await _orderRepository.Update(order);
    }

    private static void ThrowNotFound()
    {
        var message = "Não há venda cadastrada com o ID informado.";
        throw new NotFoundException(message);
    }
}