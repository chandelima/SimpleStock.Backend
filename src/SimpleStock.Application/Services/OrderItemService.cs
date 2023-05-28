using AutoMapper;
using SimpleStock.Application.Interfaces;
using SimpleStock.Data.Interfaces;
using SimpleStock.Domain.DTOs.OrderItem;
using SimpleStock.Domain.Models;
using SimpleStock.Exception;

namespace SimpleStock.Application.Services;
public class OrderItemService : IOrderItemService
{
    private readonly IProductService _productService;
    private readonly IOrderItemRepository _orderItemRepository;
    private readonly IMapper _mapper;

    public OrderItemService(
        IProductService productService,
        IOrderItemRepository orderItemRepository,
        IMapper mapper)
    {
        _productService = productService;
        _orderItemRepository = orderItemRepository;
        _mapper = mapper;
    }

    public async Task<ICollection<OrderItemResponseDto>> GetAll()
    {
        var orderItems = await _orderItemRepository.GetAll();
        return orderItems
            .Select(p => _mapper.Map<OrderItemResponseDto>(p))
            .ToList();
    }

    public async Task<OrderItemResponseDto?> GetById(Guid id)
    {
        var orderItem = await _orderItemRepository.GetById(id);
        if (orderItem == null) ThrowNotFound();

        return _mapper.Map<OrderItemResponseDto>(orderItem);
    }

    public async Task<OrderItemResponseDto?> AddOrderItem(OrderItemRequestDto request)
    {        
        var orderItemToPersist = _mapper.Map<OrderItemModel>(request);

        await _orderItemRepository.Add(orderItemToPersist);

        var orderItem = _mapper.Map<OrderItemResponseDto>(orderItemToPersist);

        return orderItem;
    }

    public async Task<OrderItemResponseDto?> UpdateOrderItem(Guid id, OrderItemRequestDto request)
    {
        var orderItem = await _orderItemRepository.GetById(id);
        if (orderItem == null) ThrowNotFound();

        _mapper.Map(request, orderItem);

        await _orderItemRepository.Update(orderItem!);

        var responseOrderItem = _mapper.Map<OrderItemResponseDto>(orderItem);

        return responseOrderItem;
    }

    public async Task<bool> DeleteOrderItem(Guid id)
    {
        var orderItem = await _orderItemRepository.GetById(id);
        if (orderItem == null)
        {
            var message = "Não há ítem de venda cadastrado com o ID informado.";
            throw new NotFoundException(message);
        }

        return await _orderItemRepository.Delete(orderItem);
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

    private async Task CheckOrderItemsHasStock(ICollection<OrderItemRequestDto> items)
    {
        foreach (var item in items)
        {
            var product = await _productService.GetById(item.ProductId);
            if (item.Amount > product!.Amount)
            {
                var message = $"A quantidade em estoque do ítem {product.Name}" +
                              $" (ID: {product.Id}) é insuficiente";
                throw new NotAllowedException(message);
            }
        }
    }
    
    private async Task<ICollection<OrderItemModel>> SetOrderItemsPrices(
        ICollection<OrderItemRequestDto> items)
    {
        ICollection<OrderItemModel> orderItems = new List<OrderItemModel>();

        foreach (var item in items)
        {
            var product = await _productService.GetById(item.ProductId);
            var orderItem = _mapper.Map<OrderItemModel>(item);
            orderItem.UnitPrice = product!.Price;

            orderItems.Add(orderItem);
        }

        return orderItems;
    }

    public async Task DecreaseOrdemItemsStock(
        ICollection<OrderItemModel> items)
    {
        var itemsRequestDto = items
            .Select(i => _mapper.Map<OrderItemRequestDto>(i))
            .ToList();

        await CheckOrderItemsHasStock(itemsRequestDto);

        foreach (var item in items)
        {
            await _productService
                .DecreaseStockAmount(item.ProductId, item.Amount);
        }
    }

    public async Task<ICollection<OrderItemModel>> ProcessOrderItems(
        ICollection<OrderItemRequestDto> items)
    {
        CheckHasDuplicatedOrderItems(items);
        await CheckOrderItemsHasStock(items);

        var orderItems = await SetOrderItemsPrices(items);

        return orderItems;
    }

    private static void ThrowNotFound()
    {
        var message = "Não há ítem de venda cadastrado com o ID informado.";
        throw new NotFoundException(message);
    }
}