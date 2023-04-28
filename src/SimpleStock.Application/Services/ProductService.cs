using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using SimpleStock.Application.Interfaces;
using SimpleStock.Data.Interfaces;
using SimpleStock.Domain.DTOs.Product;
using SimpleStock.Domain.Models;

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

    public async Task<ICollection<ProductModel>> GetByName(
        [FromBody] string name)
    {
        return await _productRepository.GetByName(name);
    }
    
    public async Task<ProductViewModel?> AddProduct(ProductInputModel input)
    {
        var productModel = _mapper.Map<ProductModel>(input);
        var result = await _productRepository.Add(productModel);

        if (!result) return null;

        return _mapper.Map<ProductViewModel>(productModel);
    }
}
