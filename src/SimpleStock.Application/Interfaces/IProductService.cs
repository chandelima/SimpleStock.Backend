using SimpleStock.Domain.DTOs.Product;
using SimpleStock.Domain.Models;
using SimpleStock.Exception;

namespace SimpleStock.Application.Interfaces;
public interface IProductService
{
    Task<ICollection<ProductResponseDto>> GetAll();
    Task<ProductResponseDto?> GetById(Guid id);
    Task<ICollection<ProductModel>> GetByName(string name);
    Task<ProductResponseDto?> AddProduct(ProductRequestDto request);
    Task<ProductResponseDto?> UpdateProduct(Guid id, ProductRequestDto request);
    Task<bool> DeleteProduct(Guid id);
}
