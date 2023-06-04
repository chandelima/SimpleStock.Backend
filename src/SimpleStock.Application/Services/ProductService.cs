using AutoMapper;
using SimpleStock.Application.Interfaces;
using SimpleStock.Data.Interfaces;
using SimpleStock.Domain.DTOs.Product;
using SimpleStock.Domain.Models;
using SimpleStock.Exception;

namespace SimpleStock.Application.Services;
public class ProductService : IProductService
{
    private readonly IProductRepository _productRepository;
    private readonly IMapper _mapper;

    public ProductService(
        IProductRepository productRepository, IMapper mapper)
    {
        _productRepository = productRepository;
        _mapper = mapper;
    }

    public async Task<ICollection<ProductResponseDto>> GetAll()
    {
        var products = await _productRepository.GetAll();
        return products
            .Select(p => _mapper.Map<ProductResponseDto>(p))
            .OrderBy(p => p.Name)
            .ToList();
    }

    public async Task<ProductResponseDto> GetById(Guid id)
    {
        var product = await _productRepository.GetById(id);
        if (product == null) ThrowNotFound(id);

        return _mapper.Map<ProductResponseDto>(product);
    }
    
    public async Task<ICollection<ProductModel>> GetByName(string name)
    {
        var product = await _productRepository.GetByName(name);
        if (product == null) ThrowNotFound();

        return product!;
    }

    public async Task<ProductResponseDto?> AddProduct(ProductRequestDto request)
    {
        await CheckIfExistsSameName(request.Name);

        var productToPersist = _mapper.Map<ProductModel>(request);
        await _productRepository.Add(productToPersist);

        var product = _mapper.Map<ProductResponseDto>(productToPersist);

        return product;
    }

    public async Task<ProductResponseDto?> UpdateProduct(Guid id, ProductRequestDto request)
    {

        var product = await _productRepository.GetById(id);
        if (product == null) ThrowNotFound(id);

        await CheckIfExistsSameName(request.Name);

        _mapper.Map(request, product);
        await _productRepository.Update(product!);

        var responseProduct = _mapper.Map<ProductResponseDto>(product);

        return responseProduct;
    }

    public async Task<bool> DeleteProduct(Guid id)
    {
        var product = await _productRepository.GetById(id);
        if (product == null) ThrowNotFound(id);

        return await _productRepository.Delete(product!);
    }

    public async Task IncreaseStockAmount(Guid productId, decimal amount)
    {
        var product = await _productRepository.GetById(productId);
        if (product == null) ThrowNotFound(productId);

        product!.Amount += amount;
        await _productRepository.Update(product);
    }

    public async Task DecreaseStockAmount(Guid productId, decimal amount)
    {
        var product = await _productRepository.GetById(productId);
        if (product == null) ThrowNotFound(productId);

        product!.Amount -= amount;
        await _productRepository.Update(product);
    }

    private static void ThrowNotFound(Guid? id = null)
    {
        var message = "Nenhum produto encontrado com os dados informados.";
        if (id != null) 
            message = $"Nenhum produto encontrado com o Id {id.ToString()}";

        throw new NotFoundException(message);
    }

    private async Task CheckIfExistsSameName(string name)
    {
        var findExistsWithName = await _productRepository.GetByName(name);
        var checkExistsWithName = findExistsWithName
            .Where(p => p.Name == name && !p.IsDeleted)
            .ToList();

        if (checkExistsWithName.Count > 0)
        {
            var message = "Já existe um ítem de produto com o nome fornecido";
            throw new AlreadyExistsException(message);
        }
    }
}
