using Microsoft.AspNetCore.Mvc;
using Toro.Domain.Interfaces.Service;
using Toro.Domain.Model;
using Microsoft.Extensions.Logging;

namespace Toro.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly IOrderProcessingService _orderService;
        private readonly ILogger<OrdersController> _logger;

        public OrdersController(IOrderProcessingService orderService, ILogger<OrdersController> logger)
        {
            _orderService = orderService ?? throw new ArgumentNullException(nameof(orderService));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        /// <summary>
        /// Cria um novo pedido para um cliente.
        /// </summary>
        /// <param name="request">Dados do pedido.</param>
        /// <returns>Resultado do pedido.</returns>
        [HttpPost]
        public async Task<IActionResult> CreateOrder([FromBody] CreateOrderRequest request)
        {
            if (request == null)
            {
                _logger.LogWarning("Requisição de pedido inválida: corpo da requisição está nulo.");
                return BadRequest("A requisição não pode ser nula.");
            }

            try
            {
                // Processa o pedido usando o serviço
                var result = await _orderService.ProcessOrderAsync(request);

                if (result.IsSuccess)
                {
                    _logger.LogInformation($"Pedido criado com sucesso. ProdutoId: {result.Response.ProductId}, Quantidade: {result.Response.Quantity}");
                    return Ok(result.Response);
                }

                // Retorna erros de validação
                _logger.LogWarning("Erro ao processar o pedido: {Errors}", string.Join(", ", result.Errors));
                return BadRequest(new { Errors = result.Errors });
            }
            catch (Exception ex)
            {
                // Loga a exceção
                _logger.LogError(ex, "Ocorreu um erro ao criar o pedido.");
                return StatusCode(500, "Ocorreu um erro interno no servidor. Por favor, tente novamente mais tarde.");
            }
        }
    }
}