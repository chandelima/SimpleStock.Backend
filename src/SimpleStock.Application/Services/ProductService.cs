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

    public async Task<ProductResponseDto?> GetById(Guid id)
    {
        var product = await _productRepository.GetById(id);
        if (product == null) ThrowNotFound();

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
        if (product == null) ThrowNotFound();

        await CheckIfExistsSameName(request.Name);

        _mapper.Map(request, product);
        await _productRepository.Update(product!);

        var responseProduct = _mapper.Map<ProductResponseDto>(product);

        return responseProduct;
    }

    public async Task<bool> DeleteProduct(Guid id)
    {
        var product = await _productRepository.GetById(id);
        if (product == null)
        {
            var message = "Não há produto cadastrado com o ID informado.";
            throw new NotFoundException(message);
        }

        return await _productRepository.Delete(product);
    }

    private static void ThrowNotFound()
    {
        var message = "Não há produto cadastrado com o ID informado.";
        throw new NotFoundException(message);
    }

    private async Task CheckIfExistsSameName(string name)
    {
        var checkExistsWithName = await _productRepository.GetByName(name);
        if (checkExistsWithName.Count > 0)
        {
            var message = "Já existe um ítem de produto com o nome fornecido";
            throw new AlreadyExistsException(message);
        }
    }
}
