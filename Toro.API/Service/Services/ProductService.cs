using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Toro.Domain.Entities;
using Toro.Domain.Interfaces.Repository;
using Toro.Domain.Interfaces.Service;

namespace Toro.Service.Services;

public class ProductService: IProductService
{
    private readonly ILogger<ProductService> _logger;
    private readonly IProductRepository _productRepository;

    public ProductService(IProductRepository productRepository)
    {
        _productRepository = productRepository;
    }

    public async Task<IEnumerable<Product?>> GetAllOrderedByTaxAsync()
    {
        try
        {
            return await _productRepository.GetAllOrderedByTaxAsync();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Erro ao buscar produtos ordenados por taxa");
            throw new Exception("Falha ao recuperar produtos", ex);
        }
    }

    public async Task<Product> GetByIdAsync(int? productId)
    {
        return await _productRepository.GetByIdAsync(productId);
    }

    public async Task UpdateAsync(Product product)
    {
        await _productRepository.UpdateAsync(product);
    }

    public async Task AddAsync(Product product)
    {
        await _productRepository.AddAsync(product);
    }

    public async Task DeleteAsync(int productId)
    {
        await _productRepository.DeleteAsync(productId);
    }
}