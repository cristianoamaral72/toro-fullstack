using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Toro.Domain.Entities;
using Toro.Domain.Interfaces.Service;

namespace Toro.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductService _productService;

        public ProductsController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpGet]
        public async Task<IActionResult> GetProducts()
        {
            try
            {
                var products = await _productService.GetAllOrderedByTaxAsync();
                return Ok(products);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetByIdAsync(int? id)
        {
            try
            {
                var product = await _productService.GetByIdAsync(id);
                return Ok(product);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut]
        public async Task<IActionResult> UpdateAsync([FromBody] Product request)
        {
            try
            {
                await _productService.UpdateAsync(request);
                return Ok("Produto editado com sucesso!!!");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> AddAsync([FromBody] Product request)
        {
            try
            {
                await _productService.AddAsync(request);
                return Ok("Product edited successfully!!!");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteAsync(int productId)
        {
            try
            {
                await _productService.DeleteAsync(productId);
                return Ok("Product deleted successfully!!!");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
